using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Newspaper : MonoBehaviour
{
    private TextMeshProUGUI text;
    GameManager gm;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        gm = FindObjectOfType<GameManager>();
    }
    public void Start()
    {
        if(gm.newspaperNews.Count == 0) text.text = "No recent tragedies have occured within the community today!";
        else
        {
            text.text = "Our community has recently been affected by the following tragedies:\n";
            foreach(string s in gm.newspaperNews)
            {
                text.text += $"\n\n{s}";
            }
        }
    }
}
