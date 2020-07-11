using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public string taskString;
    private string playerString;

    // Start is called before the first frame update
    void Start()
    {
        playerString = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (playerString == taskString)
        {
            Debug.Log("Win!");
            playerString = "";
        }
    }

    public void AddToPlayerString(string thisString)
    {
        playerString += thisString;
        Debug.Log(playerString);
    }
}
