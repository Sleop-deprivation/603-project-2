using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Newspaper : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        //textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        // Code to test if setting names works
        List<string> list = new List<string>
        {
            "Frank Furter", "Alan Smithee", "Joe Mother", "My Creative Integrity", "Jimbo from Balatro (real)"
        };
        SetListOfDead(list);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sets the list of the dead who appear on the newspaper.
    /// Using a list of srings for now since I'm not sure how dead patients
    /// will be stored since I'm working on this before that system is implemented.
    /// </summary>
    /// <param name="names">The names of the people who died.</param>
    public void SetListOfDead(List<string> names)
    {
        string newText = "";
        foreach (string name in names) 
        {
            newText += $"- {name}\n";
        }
        textMeshPro.text = newText;
    }
}
