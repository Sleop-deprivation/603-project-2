using UnityEngine;
using UnityEditor;
using System.IO;

/// <summary>
/// This script allows an automatic conversion of TSV data into Patient Scriptable Objects
/// It creates a tool at the top of the editor, "Utilities," and by clicking on the "Generate Enemies" dropdown it runs this script. 
/// The following tutorial from YouTube channel Comp-3 Interactive helped me out with this: https://www.youtube.com/watch?v=1EdLTF43d70
/// </summary>
public class TSVtoSO 
{
    [MenuItem("Utilities/Generate Patients")]
    public static void GeneratePatients()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + "/Editor/TSVs/Patients.tsv");

        // For every string, split it by the tab delimiter and read in the data. Create an asset of it afterward. 
        foreach(string s in allLines)
        {
            string[] splitData = s.Split('\t');

            SO_PatientFiles patient = ScriptableObject.CreateInstance<SO_PatientFiles>();

            patient.DayNumber = splitData[0];
            // Skip splitData[1] because that column is our names. 
            patient.FullName = splitData[2];
            patient.DOB = splitData[3];
            patient.MaritalStatus = splitData[4];
            patient.Address = splitData[5];
            patient.Gender = System.Enum.Parse<Genders>(splitData[6]);
            patient.IsEmployed = splitData[7] == "Employed" ? true : false;
            patient.HasDependents = splitData[8] == "Y" ? true : false;
            patient.Income = splitData[9];
            patient.Conditions = splitData[10];
            patient.FamilyHistory = splitData[11];
            patient.Claim = splitData[12];

            patient.HasGoldCard = splitData[13] == "Y" ? true : false;
            patient.GCIssueDate = splitData[14];
            patient.GCExpirationDate = splitData[15];
            patient.IsGCFraud = splitData[16] == "Y" ? true : false;
            // splitData[17] would be the golden card IF FRAUD: column, skip it for now

            patient.IDHeight = splitData[18];
            patient.IDEyeColor = splitData[19];
            patient.IDIssueDate = splitData[20];
            patient.IDExpirationDate = splitData[21];
            patient.IsIDFraud = splitData[22] == "Y" ? true : false;
            // splitData[23] would be the ID IF FRAUD: column, skip it for now

            patient.AcceptanceGuideline = System.Enum.Parse<Guidelines>(splitData[24]);
            patient.DenialGuideline = System.Enum.Parse<Guidelines>(splitData[25]);

            AssetDatabase.CreateAsset(patient, $"Assets/Scripts/Patients/{patient.DayNumber}/{patient.FullName}.asset");
        }
        AssetDatabase.SaveAssets();
    }
}
