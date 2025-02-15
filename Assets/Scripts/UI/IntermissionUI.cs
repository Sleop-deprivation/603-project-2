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
    [SerializeField] TextMeshProUGUI totalMoneyText;
    [SerializeField] Slider moneyJar;

    // Start is called before the first frame update
    void Start()
    {
        int dayNumber = FindObjectOfType<GameManager>().DayNum;
        int numInfractions = FindObjectOfType<GameManager>().GetRulesBroken(dayNumber);
        int moneyEarned = (300 - (numInfractions * 20));
        int totalMoney = FindObjectOfType<GameManager>().money + moneyEarned;
        dayNumberText.text = $"Day {dayNumber + 1} Complete!";
        numInfractionsText.text = $"Infractions: {numInfractions}";
        if(numInfractions == 0) moneyEarnedText.text = "Money Earned Today: $" + moneyEarned.ToString();
        else moneyEarnedText.text = $"Money Earned Today: ${moneyEarned}, with -${20 * numInfractions} deducted due to infractions.";
        totalMoneyText.text = "Total Money: $" + totalMoney.ToString();
        moneyJar.value = totalMoney / 900f;

        FindObjectOfType<GameManager>().money += moneyEarned;
    }
}
