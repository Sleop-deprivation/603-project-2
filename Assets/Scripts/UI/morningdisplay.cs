using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script controls the Dialogue System of the game. 
/// </summary>
public class MorningDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] Image dialogueIndicator;
    [SerializeField] float dialogueIndicatorTransformSpeed;
    [SerializeField] string[] lines;
    [SerializeField] float textDelay;
    private int index;
    float startingPoint;

    void Start()
    {
        textComponent.text = string.Empty;
        startingPoint = dialogueIndicator.transform.position.y;
        StartDialogue();
    }
    void Update()
    {
        AnimateDialogueIndicator();
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index]) NextLine();
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }
    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textDelay);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else this.gameObject.SetActive(false);
    }

    void AnimateDialogueIndicator()
    {
        float min = startingPoint + 5;
        float max = startingPoint - 5;
        dialogueIndicator.transform.position += new Vector3(0, dialogueIndicatorTransformSpeed * Time.deltaTime, 0);
        if (dialogueIndicator.transform.position.y >= min || dialogueIndicator.transform.position.y <= max) dialogueIndicatorTransformSpeed = -dialogueIndicatorTransformSpeed;
    }
}
