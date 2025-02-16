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
    // A reference to the GameManager.
    private GameManager gameManager;
    // approve or deny stamp
    [SerializeField] bool approveStamp;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        stampCol = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isGrabbed = false;

        if (this.name == "Approve Stamp")
            approveStamp = true;
        else
            approveStamp = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Temp code until GameManager (hopefully) includes a way to detect the current mouse position
        Vector2 mousePos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (gameManager.IsPopupActive && (gameManager.PopupActive != null) && isGrabbed)
            {
                gameManager.PopupActive.ApproveDeny(approveStamp);
            }
        }

        // When the mouse is right clicked while this popup is being hovered over:
        if (Mouse.current.leftButton.isPressed)
        {
            if (!gameManager.IsGrabbing || isGrabbed)
            {
                if (stampCol.OverlapPoint(mousePos))
                {
                    // Makes it so player can drag until they click again
                    isGrabbed = true;
                    gameManager.IsGrabbing = true;
                }
            }
        }
        else
        {
            isGrabbed = false;
            gameManager.IsGrabbing = false;
        }


        if (isGrabbed)
        {
            // Mark the popup as being grabbed
            transform.position = mousePos;
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("clicked");
        if (!context.started)
            return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (stampCol.OverlapPoint(mousePos))
            // Makes it so player can drag until they click again
            isGrabbed = !isGrabbed;
    }
}
