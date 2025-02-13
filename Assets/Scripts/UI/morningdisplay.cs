using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morningdisplay : MonoBehaviour
{
    public GameObject body;
    void Start()
    {
        StartCoroutine(Morning());
       
        
    }
    IEnumerator Morning()
    {
        body.SetActive(true);
        yield return new WaitForSeconds(3);
        body.SetActive(false);


    }
}
