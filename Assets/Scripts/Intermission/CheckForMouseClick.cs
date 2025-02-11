using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckForMouseClick : MonoBehaviour
{
    SceneChanger sceneChanger;
    float delayTime;
    float timer;

    private void Start()
    {
        sceneChanger = GetComponent<SceneChanger>();
        delayTime = FindObjectOfType<OscillateTextAlpha>().DelayTime;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delayTime && Mouse.current.leftButton.wasPressedThisFrame)
        {
            sceneChanger.GoToPreviousScene();
        }
    }
}
