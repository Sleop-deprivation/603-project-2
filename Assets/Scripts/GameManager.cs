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
    public List<List<string>> patientstatus = new List<List<string>>();

    private bool isPopupActive;
    private bool isGamePaused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject popupBackground;

    int filesTurnedIn;
    public int FilesTurnedIn { get { return filesTurnedIn; } set { filesTurnedIn = value; } }
    public bool IsPopupActive
    {
        get => isPopupActive;
        set
        {
            isPopupActive = value;
            popupBackground.SetActive(value);
        }
    }

    public bool IsGamePaused
    {
        get => isGamePaused;
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

        pauseMenu.SetActive(false);
        isGamePaused = false;

        patients.Add(Day1Patients);
        patients.Add(Day2Patients);
        patients.Add(Day3Patients);
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
        if(filesTurnedIn == 10)
        {
            filesTurnedIn = 0;
            CheckEndOfDay();
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
            file.GetComponent<DisplayPatientFiles>().Assign(patients[dayNumber][i]);
            ++i;
        }
        FindObjectOfType<DailyGuidelinesUpdater>().UpdateText(dayNumber);
    }
    void CheckEndOfDay()
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
        FindObjectOfType<SceneChanger>().GoToNextScene();
    }
    public void PauseGame()
    {
        isGamePaused = true;
        pauseMenu.SetActive(true);
    }

    public void UnpauseGame()
    {
        isGamePaused = false;
        pauseMenu.SetActive(false);
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
