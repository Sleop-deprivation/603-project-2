using UnityEngine;

public class NewDayStarted : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<GameManager>().UpdateDay();
    }
}
