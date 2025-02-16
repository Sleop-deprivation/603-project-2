using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    static GameManager instance;
    static int dayNumber = -1;
    public int DayNum { get { return dayNumber; } }

    public int money,bribes,rulesbrokenday1,rulesbrokenday2,rulesbrokenday3;
    [SerializeField] SO_PatientFiles[] Day1Patients;
    [SerializeField] SO_PatientFiles[] Day2Patients;
    [SerializeField] SO_PatientFiles[] Day3Patients;
    List<SO_PatientFiles[]> patients = new List<SO_PatientFiles[]>();
    GameObject Clockout;
    //public List<List<string>> patientstatus = new List<List<string>>();

    private bool isPopupActive;
    private bool isGrabbing;
    private bool isGamePaused;
    private GameObject popupBackground;

    private Popup popupActive;

    public bool IsPopupActive
    {
        get => isPopupActive;
        set
        {
            isPopupActive = value;
            if (value)
                popupBackground.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.5f);
            else
                popupBackground.GetComponent<SpriteRenderer>().color = Color.clear;
        }
    }

    public bool IsGamePaused
    {
        get => isGamePaused;
    }

    public bool IsGrabbing
    {
        get => isGrabbing;
        set { isGrabbing = value; }
    }

    public Popup PopupActive
    {
        get => popupActive;
        set { popupActive = value; }
    }

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

        isGamePaused = false;
        isGrabbing = false;

        patients.Add(Day1Patients);
        patients.Add(Day2Patients);
        patients.Add(Day3Patients);

        if (popupBackground == null)
            popupBackground = GameObject.FindWithTag("PopUpBackground");
        popupBackground.GetComponent<SpriteRenderer>().color = Color.clear;

        Clockout = GameObject.FindWithTag("Clockout");
        Clockout.SetActive(false);
    }

    private void Update()
    {
        // When the escape key is pressed, toggle the game's paused state
        if (UnityEngine.InputSystem.Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isGamePaused)
                PauseGame();
            else
                UnpauseGame();
        }
        // Continuously check patient files to see if they are all stamped
        if(CheckForAllPatientsStamped())
        {
            // Display End Day Button
            Clockout.SetActive(true);
        }
    }

    /// <summary>
    /// This should be called when a new day starts
    /// </summary>
    public void UpdateDay()
    {
        dayNumber++;
        int i = 0;
        Transform patientFiles = GameObject.FindGameObjectWithTag("PatientFiles").transform;
        foreach(Transform file in patientFiles)
        {
            if (i >= patients[dayNumber].Length) { file.gameObject.SetActive(false); }
            else { file.GetComponent<DisplayPatientFiles>().Assign(patients[dayNumber][i]); ++i; }
        }
        FindObjectOfType<DailyGuidelinesUpdater>().UpdateText(dayNumber);
        // If the reference to the popup background was lost, update its reference.
        // The background has to be active in order for this to work, so don't make
        // the popup background inactive in the editor when building please
        if (popupBackground == null)
        {
            popupBackground = GameObject.FindWithTag("PopUpBackground");
            popupBackground.GetComponent<SpriteRenderer>().color = Color.clear;
        }
        if(Clockout == null)
        {
            Clockout = GameObject.FindWithTag("Clockout");
            Clockout.SetActive(false);
        }
    }

    bool CheckForAllPatientsStamped()
    {
        if (dayNumber == 0)
        {
            int count = 0;
            foreach(SO_PatientFiles patient in Day1Patients)
            {
                if (patient.IsStamped) count++;
                if (count == Day1Patients.Length) return true;
            }
        }
        else if (dayNumber == 1)
        {
            int count = 0;
            foreach (SO_PatientFiles patient in Day2Patients)
            {
                if (patient.IsStamped) count++;
                if (count == Day2Patients.Length) return true;
            }
        }
        else if (dayNumber == 2)
        {
            int count = 0;
            foreach (SO_PatientFiles patient in Day3Patients)
            {
                if (patient.IsStamped) count++;
                if (count == Day3Patients.Length) return true;
            }
        }
        return false;
    }

    public void CheckEndOfDay()
    {
        if (dayNumber == 0)
        {
            foreach (SO_PatientFiles patient in Day1Patients)
            {
                GetComponent<DataTracking>().RecordData(patient);
                if (patient.AcceptanceGuideline != Guidelines.None && !patient.IsDenied) rulesbrokenday1++;
                if (patient.DenialGuideline != Guidelines.None && patient.IsDenied) rulesbrokenday1++;
            }
        }
        else if (dayNumber == 1)
        {
            foreach (SO_PatientFiles patient in Day2Patients)
            {
                GetComponent<DataTracking>().RecordData(patient);
                if (patient.AcceptanceGuideline != Guidelines.None && !patient.IsDenied) rulesbrokenday2++;
                if (patient.DenialGuideline != Guidelines.None && patient.IsDenied) rulesbrokenday2++;
            }
        }
        else if (dayNumber == 2)
        {
            foreach (SO_PatientFiles patient in Day3Patients)
            {
                GetComponent<DataTracking>().RecordData(patient);
                if (patient.AcceptanceGuideline != Guidelines.None && !patient.IsDenied) rulesbrokenday3++;
                if (patient.DenialGuideline != Guidelines.None && patient.IsDenied) rulesbrokenday3++;
            }
        }
        FindObjectOfType<SceneChanger>().GoToNextScene();
    }
    public void PauseGame()
    {
        isGamePaused = true;
    }

    public void UnpauseGame()
    {
        isGamePaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public int GetRulesBroken(int dayNum)
    {
        if (dayNum == 0) return rulesbrokenday1;
        else if (dayNum == 1) return rulesbrokenday2;
        else if (dayNum == 2) return rulesbrokenday3;
        return -1;
    }
}
