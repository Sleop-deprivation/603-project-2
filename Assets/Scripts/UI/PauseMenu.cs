using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        canvas = GetComponentInChildren<Canvas>().gameObject;
        HideMenu();
    }

    // Update is called once per frame
    void Update()
    {
        // When the escape key is pressed, toggle the game's paused state
        if (UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!gameManager.IsGamePaused)
            {
                ShowMenu();
                gameManager.PauseGame();
            }
            else
            {
                HideMenu();
                gameManager.UnpauseGame();
            }
        }
    }

    public void HideMenu()
    {
        GetComponent<SpriteRenderer>().color = Color.clear;
        canvas.SetActive(false);
    }

    public void ShowMenu()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
        canvas.SetActive(true);
    }
}
