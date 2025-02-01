using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Very basic struct that stores all three components of a transform.
/// Pretty much just makes it so that I don't have to make three seperate
/// fields to store the reference to the popup's transform before being
/// brought into focus.
/// </summary>
struct TransformComponents
{
    public Vector3 Pos { get; set; }
    public Quaternion Rot { get; set; }
    public Vector3 Scale { get; set; }

    public void SetComponents(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        Pos = pos;
        Rot = rot;
        Scale = scale;
    }
}

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

    // ~~~ PROPERTIES ~~~
    /// <summary>
    /// Public getter/setter for the popup's sprite.
    /// </summary>
    public Sprite Texture
    {
        get { return spriteRenderer.sprite; }
        set { spriteRenderer.sprite  = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        preFocusTransform = new TransformComponents();
        popupCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // If not in-focus, check to see if the mouse is hovering the object
        if (!inFocus)
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

                    // Store references to the transform values from before it was in focus
                    preFocusTransform.SetComponents(transform.position, transform.rotation, transform.localScale);

                    // Make it display in the center of the screen
                    transform.position = Vector2.zero;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    transform.localScale = Vector2.one;
                }
            }
            else if (spriteRenderer.color != Color.white)
            {
                spriteRenderer.color = Color.white;
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
            }    
        }
    }
}
