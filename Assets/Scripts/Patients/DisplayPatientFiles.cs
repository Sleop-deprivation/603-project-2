using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayPatientFiles : MonoBehaviour
{
    Popup patientPopUp;
    GameObject InsuranceClaim;
    GameObject GoldenInsuranceCard;
    GameObject Background;

    GameObject ClaimsUI;
    GameObject ClaimTexts;
    TextMeshProUGUI fullName;
    TextMeshProUGUI dob;
    TextMeshProUGUI maritalStatus;
    TextMeshProUGUI address;
    GameObject gender;
    GameObject employment;
    GameObject dependants;
    TextMeshProUGUI income;
    TextMeshProUGUI preExistingConditions;
    TextMeshProUGUI familyHealthHistory;

    GameObject GoldCardUI;
    GameObject GCTexts;
    TextMeshProUGUI gcFullName;
    TextMeshProUGUI gcDOB;
    TextMeshProUGUI gcAddress;
    TextMeshProUGUI gcDateIssued;
    TextMeshProUGUI gcDateExpires;

    List<GameObject> documents = new List<GameObject>();

    private void Awake()
    {
        patientPopUp = GetComponent<Popup>();
        InsuranceClaim = transform.GetChild(1).gameObject;
        GoldenInsuranceCard = transform.GetChild(2).gameObject;
        Background = transform.GetChild(3).gameObject;

        ClaimsUI = transform.GetChild(0).GetChild(0).gameObject;
        ClaimTexts = ClaimsUI.transform.GetChild(0).gameObject;

        GoldCardUI = transform.GetChild(0).GetChild(1).gameObject;
        GCTexts = GoldCardUI.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(patientPopUp.InFocus)
        {
            foreach(GameObject o in documents)
            {
                o.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject o in documents)
            {
                o.SetActive(false);
            }
        }
    }
    /// <summary>
    /// Assign patient data
    /// </summary>
    /// <param name="patient"></param>
    public void Assign(SO_PatientFiles patient)
    {
        // Add appropriate documents to be displayed here. 
        documents.Add(InsuranceClaim);
        if (patient.HasGoldCard) documents.Add(GoldenInsuranceCard);
        documents.Add(Background);

        this.gameObject.name = patient.FullName;
    }
}