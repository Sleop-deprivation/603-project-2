using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatientFile", menuName = "ScriptableObjects/PatientFile")]
public class SO_PatientFiles : ScriptableObject
{
    // Patient Information
    public string DayNumber;
    public string FullName;
    public string DOB;
    public string MaritalStatus;
    public string Address;
    public Genders Gender;
    public bool IsEmployed;
    public bool HasDependents;
    public string Income;
    public string Conditions;
    public string FamilyHistory;
    public string Claim;

    // Gold Card Information
    public bool HasGoldCard;
    public string GCIssueDate;
    public string GCExpirationDate;
    public bool IsGCFraud;

    // ID Information
    public string IDHeight;
    public string IDEyeColor;
    public string IDIssueDate;
    public string IDExpirationDate;
    public bool IsIDFraud;

    // Guidelines Information
    public Guidelines AcceptanceGuideline;
    public Guidelines DenialGuideline;
}
