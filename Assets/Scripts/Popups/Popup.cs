using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
public class Popup : MonoBehaviour
{
    // ~~~ FIELDS ~~~
    // Whether or not the popup is in focus or not.
    private bool inFocus;
    // The transform of the popup before it was in-focus.
    private TransformComponents preFocusTransform;
    // The collider attached to this popup.
    private Collider2D popupCollider;
    // The SpriteRenderer attached to this popup.
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private TransformComponents inFocusTransform;
    // A reference to the GameManager.
    private GameManager gameManager;

    // ~~~ PROPERTIES ~~~
    /// <summary>
    /// Public getter/setter for the popup's sprite.
    /// </summary>
    public Sprite Texture
    {
        get { return spriteRenderer.sprite; }
        set { spriteRenderer.sprite  = value; }
    }
    public bool InFocus { get { return inFocus; } }

    // Start is called before the first frame update
    void Start()
    {
        preFocusTransform = ScriptableObject.CreateInstance<TransformComponents>();
        popupCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (inFocusTransform == null)
        {
            inFocusTransform = ScriptableObject.CreateInstance<TransformComponents>();
            inFocusTransform.SetComponents(Vector2.zero, Quaternion.Euler(0, 0, 0), Vector2.one);
        }
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // If not in-focus and while no other popups are active, check to see if the mouse is hovering the object
        if (!inFocus)
        {
            // This wasn't working with the && operator so I made it a seperate if statement for now
            if (!gameManager.IsPopupActive && !gameManager.IsGamePaused) 
            {
                // Temp code until GameManager (hopefully) includes a way to detect the current mouse position
                Vector2 mousePos = Mouse.current.position.ReadValue();
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                // Check to see if the mouse is hovering over the popup
                if (popupCollider.OverlapPoint(mousePos))
                {
                    // Highlighr the sprite green if it is being hovered over
                    if (spriteRenderer.color != Color.green)
                        spriteRenderer.color = Color.green;

                    // When the mouse is clicked while this popup is being hovered over:
                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        // Mark the popup as in focus
                        inFocus = true;
                        spriteRenderer.color = Color.white;
                        gameManager.IsPopupActive = true;

                        // Store references to the transform values from before it was in focus
                        preFocusTransform.SetComponents(transform.position, transform.rotation, transform.localScale);

                        // Make it display in the center of the screen
                        transform.position = inFocusTransform.Pos;
                        transform.rotation = inFocusTransform.Rot;
                        transform.localScale = inFocusTransform.Scale;

                        // Set high priority sorting order when focused
                        spriteRenderer.sortingOrder = 5;
                    }
                }
                else if (spriteRenderer.color != Color.white)
                {
                    spriteRenderer.color = Color.white;
                }
            }
        }
        else
        {
            // When pressing the mouse button while this popup is in focus, un-focus it
            // and return it to its original position
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                inFocus = false;
                transform.position = preFocusTransform.Pos;
                transform.rotation = preFocusTransform.Rot;
                transform.localScale = preFocusTransform.Scale;
                spriteRenderer.sortingOrder = 0;
                gameManager.IsPopupActive = false;
            }    
        }
    }
}
