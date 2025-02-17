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
            text.text = "1. If a patient has a gold insurance card, they must always be approved." +
                        "\n2. Check for invalid information in patient files. If any information seems incorrect, the patient must be denied.";
        }
        else if(dayNum == 1)
        {
            text.text = "1. If a patient has a gold insurance card, they must always be approved." +
                        "\n2.Check for invalid information in patient files.If any information seems incorrect, the patient must be denied." +
                        "\n3. Patients above 70 years of age must be denied." + 
                        "\n4. Patients above 30 who are not married must be denied." +
                        "\n5. Patients above 30 who have no dependents must be denied.";
        }
        else if(dayNum == 2 || dayNum == 3)
        {
            text.text = "1. If a patient has a gold insurance card, they must always be approved." +
                        "\n2.Check for invalid information in patient files.If any information seems incorrect, the patient must be denied." +
                        "\n3. Patients above 70 years of age must be denied." +
                        "\n4. Patients above 30 who are not married must be denied." +
                        "\n5. Patients above 30 who have no dependents must be denied." +
                        "\n6. If a patient's current employment status is “unemployed”, they must be denied." +
                        "\n7. If a patient’s Annual Income is below 40k, they must be denied." +
                        "\n8. Patients with who’s insurance claim is related to pre - existing conditions must be denied." +
                        "\n9. Patients who’s current insurance claim is related to their family health history must be denied.";
        }
    }
}
