using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is meant for whole entire UI Game Objects to oscillate in opacity. 
/// Not to be confused with the more specific functionality of OscillateTextAlpha.cs script. 
/// </summary>
public class FadeInAndOut : MonoBehaviour
{
    [SerializeField] float oscillationSpeed = 1f;
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(OscillateOpacity(oscillationSpeed));
    }

    IEnumerator OscillateOpacity(float oscillationSpeed)
    {
        float oscillatingAlpha = 0.5f * (Mathf.Cos((Time.time * oscillationSpeed) - (Mathf.PI / 2)) + 1);
        image.color = new Color(image.color.r, image.color.g, image.color.b, oscillatingAlpha);
        yield return null;
    }
}
