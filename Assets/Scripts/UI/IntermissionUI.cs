using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntermissionUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dayNumberText;
    [SerializeField] TextMeshProUGUI numInfractionsText;
    [SerializeField] TextMeshProUGUI moneyEarnedText;
    [SerializeField] Slider moneyJar;

    // Start is called before the first frame update
    void Start()
    {
        int dayNumber = FindObjectOfType<GameManager>().DayNum;
        dayNumberText.text = $"Day {dayNumber + 1} Complete!";
        numInfractionsText.text = $"Infractions: {FindObjectOfType<GameManager>().GetRulesBroken(dayNumber)}";
    }
}
