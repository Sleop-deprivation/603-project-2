using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DailyGuidelinesUpdater : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText(int dayNum)
    {
        if(dayNum == 0)
        {
            text.text = "1. If a patient has a gold insurance card, they must always be approved.";
        }
        else if(dayNum == 1)
        {
            text.text = "1. If a patient has a gold insurance card, they must always be approved." +
                        "\n2. Patients above 70 years of age must be denied." + 
                        "\n3. Patients above 30 who are not married must be denied." +
                        "\n4. Patients above 30 who have no dependents must be denied.";
        }
    }
}
