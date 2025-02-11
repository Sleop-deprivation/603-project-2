using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OscillateTextAlpha : MonoBehaviour
{
    [SerializeField] float delayTime = 0f;
    [SerializeField] float oscillationSpeed = 1f;
    TextMeshProUGUI text;

    public float DelayTime { get { return delayTime; } }

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(OscillateOpacity(delayTime, oscillationSpeed));
    }

    IEnumerator OscillateOpacity(float delayTime = 0, float oscillationSpeed = 1)
    {
        yield return new WaitForSeconds(delayTime);

        //Normalizes oscillations between 0 and 1 instead of -1 and 1
        float textAlpha = 0.5f * (Mathf.Cos((Time.time * oscillationSpeed) - (Mathf.PI / 2)) + 1);
        text.alpha = textAlpha;
        yield return null;
    }
}
