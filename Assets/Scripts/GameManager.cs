using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    static GameManager instance;
    static int dayNumber = -1;

    public int money,bribes,rulesbrokenday1,rulesbrokenday2,rulesbrokenday3;
    [SerializeField] GameObject[] patientFiles;
    [SerializeField] SO_PatientFiles[] Day1Patients;
    [SerializeField] SO_PatientFiles[] Day2Patients;
    [SerializeField] SO_PatientFiles[] Day3Patients;
    List<SO_PatientFiles[]> patients = new List<SO_PatientFiles[]>();
    public List<List<string>> patientstatus = new List<List<string>>();
    //Make sure the GameObject remains intact between scenes
    void Awake()
    {
        if (instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
           instance=this;
           DontDestroyOnLoad(gameObject);
        }

        patients.Add(Day1Patients);
        patients.Add(Day2Patients);
        patients.Add(Day3Patients);
    }

    /// <summary>
    /// This should be called when a new day starts
    /// </summary>
    public void UpdateDay()
    {
        dayNumber++;
        int i = 0;
        foreach(GameObject file in patientFiles)
        {
            file.GetComponent<DisplayPatientFiles>().Assign(patients[dayNumber][i]);
            ++i;
        }
    }
    

}
