using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stamp : MonoBehaviour
{
    private SpriteRenderer stamp;
    private Collider2D stampCollider;
    private bool bStamped;

    public Sprite approve;
    public Sprite deny;

    // Start is called before the first frame update
    void Start()
    {
        stampCollider = GetComponent<Collider2D>();
        stamp = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        bStamped = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Temp code until GameManager (hopefully) includes a way to detect the current mouse position
        Vector2 mousePos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // Check to see if the mouse is hovering over the popup
        if (stampCollider.OverlapPoint(mousePos))
        {
            // Check if alr stamped
            if (!bStamped)
            {
                if (Keyboard.current.aKey.wasPressedThisFrame)
                {
                    // Set stamp to approved
                    bStamped = true;
                    stamp.sprite = approve;
                }
                if (Keyboard.current.dKey.wasPressedThisFrame)
                {
                    // Set stamp to denied
                    bStamped = true;
                    stamp.sprite = deny;
                }
            }

        }
    }
}