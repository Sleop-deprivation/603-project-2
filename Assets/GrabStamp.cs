using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabStamp : MonoBehaviour
{
    // The collider attached
    private Collider2D stampCol;
    // The SpriteRenderer attached
    private SpriteRenderer spriteRenderer;
    // Whether or not this has been grabbed
    public bool isGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        stampCol = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Temp code until GameManager (hopefully) includes a way to detect the current mouse position
        Vector2 mousePos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // When the mouse is right clicked while this popup is being hovered over:
        if (Mouse.current.rightButton.wasPressedThisFrame)
            if (stampCol.OverlapPoint(mousePos))
                // Makes it so player can drag until they click again
                isGrabbed = !isGrabbed;

        if (isGrabbed)
        {
            // Mark the popup as being grabbed
            transform.position = mousePos;
        }
    }
}
