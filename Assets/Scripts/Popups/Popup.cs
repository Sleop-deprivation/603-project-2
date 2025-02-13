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
    // Whether or not the popup has been submitted
    private bool submitted;
    // Whether or not the popup has been grabbed
    private bool isGrabbed;
    // Reference to Approve stamp
    private GameObject approveStamp;
    // Reference to Approve stamp
    private GameObject denyStamp;
    // Is this approved?
    public bool bApproved;
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

    [SerializeField] bool isClipped;

    // ~~~ PROPERTIES ~~~
    /// <summary>
    /// Public getter/setter for the popup's sprite.
    /// </summary>
    public Sprite Texture
    {
        get { return spriteRenderer.sprite; }
        set { spriteRenderer.sprite = value; }
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
        submitted = false;
        isGrabbed = false;
        approveStamp = GameObject.Find("Approve Stamp");
        denyStamp = GameObject.Find("Deny Stamp");
        bApproved = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If already submitted just return so player can not interact.
        if (submitted) return;

        // If highlighted and stamp NOT selected just return so player can not interact.
        if (!inFocus && (approveStamp.GetComponent<GrabStamp>().isGrabbed || denyStamp.GetComponent<GrabStamp>().isGrabbed)) return;

        // If not in-focus and while no other popups are active, check to see if the mouse is hovering the object
        if (!inFocus)
        {
            // This wasn't working with the && operator so I made it a seperate if statement for now
            if (!gameManager.IsPopupActive && !gameManager.IsGamePaused)
            {
                // Temp code until GameManager (hopefully) includes a way to detect the current mouse position
                Vector2 mousePos = Mouse.current.position.ReadValue();
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                // When the mouse is right clicked while this popup is being hovered over:
                if (Mouse.current.rightButton.wasPressedThisFrame && !isClipped)
                    if (popupCollider.OverlapPoint(mousePos))
                        // Makes it so player can drag until they click again
                        isGrabbed = !isGrabbed;

                if (isGrabbed)
                {
                    // Mark the popup as being grabbed
                    transform.position = mousePos;
                    spriteRenderer.color = Color.white;

                    // Set high priority sorting order when grabbed
                    spriteRenderer.sortingOrder = 7;

                    ContactFilter2D filter = new ContactFilter2D();
                    filter.SetLayerMask(LayerMask.GetMask("ApproveBox"));
                    List<Collider2D> results = new List<Collider2D>();

                    // check if it overlaps with the boxes to submit files
                    popupCollider.OverlapCollider(filter, results);
                    if (results.Count != 0)
                    {
                        // Check if document is approved or not and compare it to which bin it is entering
                        if ((results[0].CompareTag("Approve Bin")) && bApproved)
                        {
                            submitted = true;
                            transform.position = results[0].transform.position;
                        }
                        if ((results[0].name == "Denied Bin") && !bApproved)
                        {
                            submitted = true;
                            transform.position = results[0].transform.position;
                        }
                    }
                }
                else
                {
                    spriteRenderer.sortingOrder = 0;
                    // Check to see if the mouse is hovering over the popup
                    if (popupCollider.OverlapPoint(mousePos))
                    {
                        // Highlighr the sprite green if it is being hovered over
                        if (spriteRenderer.color != Color.green) spriteRenderer.color = Color.green;

                        // When the mouse is clicked while this popup is being hovered over:
                        if (Mouse.current.leftButton.wasPressedThisFrame)
                        {
                            // Mark the popup as in focus
                            inFocus = true;
                            spriteRenderer.color = Color.white;

                            // Store references to the transform values from before it was in focus
                            preFocusTransform.SetComponents(transform.position, transform.rotation, transform.localScale);

                            // Make it display in the center of the screen
                            transform.position = inFocusTransform.Pos;
                            transform.rotation = inFocusTransform.Rot;
                            transform.localScale = inFocusTransform.Scale;

                            // Set high priority sorting order when focused
                            spriteRenderer.sortingOrder = 5;
                            gameManager.IsPopupActive = true;
                        }
                    }
                    else if (spriteRenderer.color != Color.white) spriteRenderer.color = Color.white;
                }
            }
        }

        // When pressing the mouse button while this popup is in focus, un-focus it
        // and return it to its original position only when no stamp is held
        else
        {
            // if left click while approve is grabbed approve
            if (Mouse.current.leftButton.wasPressedThisFrame && approveStamp.GetComponent<GrabStamp>().isGrabbed) this.GetComponentInChildren<Stamp>().StampApprove(true);
            // if left click while deny is grabbed deny
            if (Mouse.current.leftButton.wasPressedThisFrame && denyStamp.GetComponent<GrabStamp>().isGrabbed) this.GetComponentInChildren<Stamp>().StampApprove(false);

            if (Mouse.current.leftButton.wasPressedThisFrame && (!approveStamp.GetComponent<GrabStamp>().isGrabbed && !denyStamp.GetComponent<GrabStamp>().isGrabbed))
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