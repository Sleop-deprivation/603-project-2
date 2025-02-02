using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stamp : MonoBehaviour
{
    private SpriteRenderer stamp;
    private Collider2D stampCollider;

    public bool bApproved;
    public Sprite approve;
    public Sprite deny;

    // Start is called before the first frame update
    void Start()
    {
        stampCollider = GetComponent<Collider2D>();
        stamp = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
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
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                // Set stamp to approved
                bApproved = true;
                stamp.sprite = approve;
            }
            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                // Set stamp to denied
                bApproved = false;
                stamp.sprite = deny;
            }

        }
    }
}