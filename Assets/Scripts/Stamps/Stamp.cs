using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stamp : MonoBehaviour
{
    private SpriteRenderer stamp;
    private SO_PatientFiles patient;
    public bool bApproved;
    public Sprite approve;
    public Sprite deny;

    public GameObject gameManager;
    public List<string> status = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        patient = transform.parent.GetComponent<DisplayPatientFiles>().CurrentPatient;
        stamp = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        gameManager=GameObject.Find("GameManager");
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            // Set stamp to approved
            patient.IsDenied = false;
            stamp.sprite = approve;
            if (status.Count==0)
            {
                status.Add(patient.FullName);
                status.Add("approved");
                gameManager.GetComponent<GameManager>().patientstatus.Add(status);
              


            }
           
            gameManager.GetComponent<GameManager>().patientstatus.Add(status);
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            // Set stamp to denied
            patient.IsDenied = true;
            stamp.sprite = deny;
             if (status.Count==0)
            {
                status.Add(patient.FullName);
                status.Add("denied");
                gameManager.GetComponent<GameManager>().patientstatus.Add(status);

            }
            
        }
    }
}