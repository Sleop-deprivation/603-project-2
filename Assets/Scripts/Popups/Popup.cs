using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Popup : MonoBehaviour
{
    // ~~~ FIELDS ~~~
    // Whether or not the popup is in focus or not.
    private bool inFocus;
    // The transform of the popup before it was in-focus.
    private Transform preFocusTransform;
    // The collider of this popup.
    private Collider2D popupCollider;

    // ~~~ PROPERTIES ~~~
    /// <summary>
    /// Public getter/setter for the popup's sprite.
    /// </summary>
    public Sprite Texture
    {
        get { return GetComponent<SpriteRenderer>().sprite; }
        set { GetComponent<SpriteRenderer>().sprite  = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        popupCollider = GetComponent<Collider2D>();
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
                GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
