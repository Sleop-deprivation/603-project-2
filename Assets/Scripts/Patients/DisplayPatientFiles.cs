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
    /// Assign patient data when the day starts. 
    /// </summary>
    /// <param name="patient"></param>
    public void Assign(SO_PatientFiles patient)
    {
        this.gameObject.name = patient.FullName;

        // Add appropriate documents to be displayed here. 
        documents.Add(InsuranceClaim);
        documents.Add(ClaimsUI);
        documents.Add(Background);

        // Filling out Insurance Claim text boxes
        int i = 0;
        fullName = ClaimTexts.transform.GetChild(i++).GetComponent<TextMeshProUGUI>();
        fullName.text = patient.FullName;

        dob = ClaimTexts.transform.GetChild(i++).GetComponent<TextMeshProUGUI>();
        dob.text = patient.DOB;

        maritalStatus = ClaimTexts.transform.GetChild(i++).GetComponent<TextMeshProUGUI>();
        maritalStatus.text = patient.MaritalStatus;

        address = ClaimTexts.transform.GetChild(i++).GetComponent<TextMeshProUGUI>();
        address.text = patient.Address;

        gender = ClaimTexts.transform.GetChild(i++).gameObject;
        if (patient.Gender == Genders.Female) gender.transform.GetChild(0).gameObject.SetActive(true);
        else if (patient.Gender == Genders.Female) gender.transform.GetChild(1).gameObject.SetActive(true);
        else gender.transform.GetChild(2).gameObject.SetActive(true);

        employment = ClaimTexts.transform.GetChild(i++).gameObject;
        if (patient.IsEmployed) employment.transform.GetChild(0).gameObject.SetActive(true);
        else employment.transform.GetChild(1).gameObject.SetActive(true);

        dependants = ClaimTexts.transform.GetChild(i++).gameObject;
        if (patient.HasDependents) dependants.transform.GetChild(0).gameObject.SetActive(true);
        else dependants.transform.GetChild(1).gameObject.SetActive(true);

        income = ClaimTexts.transform.GetChild(i++).GetComponent<TextMeshProUGUI>();
        income.text = patient.Income;

        preExistingConditions = ClaimTexts.transform.GetChild(i++).GetComponent<TextMeshProUGUI>();
        preExistingConditions.text = patient.Conditions;

        familyHealthHistory = ClaimTexts.transform.GetChild(i++).GetComponent<TextMeshProUGUI>();
        familyHealthHistory.text = patient.FamilyHistory;

        // If Patient has a gold card...
        if (patient.HasGoldCard) { 
            documents.Add(GoldenInsuranceCard); 
            documents.Add(GoldCardUI);
            i = 0;

            gcFullName = GCTexts.transform.GetChild(i++).GetComponent<TextMeshProUGUI>();
            gcFullName.text = patient.FullName;
            gcDOB = GCTexts.transform.GetChild(i++).GetComponent<TextMeshProUGUI>();
            gcDOB.text = patient.DOB;
            gcAddress = GCTexts.transform.GetChild(i++).GetComponent<TextMeshProUGUI>();
            gcAddress.text = patient.Address;
            gcDateIssued = GCTexts.transform.GetChild(i++).GetComponent<TextMeshProUGUI>();
            gcDateIssued.text = patient.GCIssueDate;
            gcDateExpires = GCTexts.transform.GetChild(i++).GetComponent<TextMeshProUGUI>();
            gcDateExpires.text = patient.GCExpirationDate;
        }
    }
}