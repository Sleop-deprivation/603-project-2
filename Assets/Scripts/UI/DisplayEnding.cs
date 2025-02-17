using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnding : MonoBehaviour
{
    Image image;
    [SerializeField] SO_PatientFiles daughter;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
        if(daughter.IsDenied) image.sprite = Resources.Load<Sprite>("Ending1");
        else image.sprite = Resources.Load<Sprite>("Ending2");
    }
}
