using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuidelineSheet : MonoBehaviour
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
            "Allow all gold card members", 
            "Old people aren't allowed to be healthy", 
            "Do not question your superiors", 
            "Drink all the shampoo in the house", 
            "Ignore Rule 4",
            "Don't ignore Rule 4 actually it'd be really funny trust",
            "Leo is no longer allowed to make rules"
        };
        SetGuidelines(list);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Sets the list of guidelines. Using a list of srings for now since I'm not sure
    /// how guidelines will be stored since I'm working on this before that system is 
    /// implemented. Yes, I copy-pasted this code from the Newspaper class.
    /// </summary>
    /// <param name="rules">The rules to add to the document.</param>
    public void SetGuidelines(List<string> rules)
    {
        string newText = "";
        for (int i = 0; i < rules.Count; i++)
        {
            string rule = rules[i]; 
            newText += $"{i+1}) {rule}\n";
        }
        textMeshPro.text = newText;
    }
}
