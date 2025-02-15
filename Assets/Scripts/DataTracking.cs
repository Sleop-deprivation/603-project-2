using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class DataTracking : MonoBehaviour
{
    /// <summary>
    /// Records DayNumber, Name, Denial/Acceptance Status, and the Guideline that the player breaks for each patient. 
    /// If no guideline was broken, then "None" is recorded. 
    /// Data is recorded to "{Drive}:\Users\{user}\AppData\LocalLow\DefaultCompany\603_Project_2.csv"
    /// </summary>
    /// <param name="patient"></param>
    public void RecordData(SO_PatientFiles patient)
    {
        string guideLine;
        // If there is a penalty for accepting the patient, and the patient is accepted... 
        if (patient.AcceptanceGuideline != Guidelines.None && !patient.IsDenied) guideLine = patient.AcceptanceGuideline.ToString();
        // If there is a penalty for denying the patient, and the patient is denied... 
        else if (patient.DenialGuideline != Guidelines.None && patient.IsDenied) guideLine = patient.DenialGuideline.ToString();
        else guideLine = "None";

        StreamWriter writer = null;
        try
        {
            writer = new StreamWriter(GetPath(), append: true);
            string saveData = String.Format("{0},{1},{2},{3}", patient.DayNumber, patient.FullName, patient.IsDenied, guideLine);
            writer.WriteLine(saveData);
            print("Successfully wrote to " + GetPath());
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }

        // If save was successful.
        if (writer != null) writer.Close();
    }

    private string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, "IGME_603_Team5_TrackingData.csv");
    }
}
