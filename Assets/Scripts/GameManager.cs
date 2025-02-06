using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    static int dayNumber = -1;

    [SerializeField] GameObject[] patientFiles;
    [SerializeField] SO_PatientFiles[] Day1Patients;
    [SerializeField] SO_PatientFiles[] Day2Patients;
    [SerializeField] SO_PatientFiles[] Day3Patients;
    List<SO_PatientFiles[]> patients = new List<SO_PatientFiles[]>();
    private bool isPopupActive;
    private bool isGamePaused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject popupBackground;
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
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isGamePaused)
                PauseGame();
            else
                UnpauseGame();
        }
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
}
