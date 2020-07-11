using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public TextMeshProUGUI playerStringTMP;
    public TextMeshProUGUI taskStringTMP;

    public string taskString;

    private string playerString;

    // Start is called before the first frame update
    void Start()
    {
        playerString = "";

        taskString = CreateNewTask(5);
        taskStringTMP.text = "Task: " + taskString;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToPlayerString(string thisString)
    {
        playerString += thisString;
        // Debug.Log(playerString);
        playerStringTMP.text = "Player: " + playerString;
    }

    public void ResetPlayerString()
    {
        playerString = "";
        playerStringTMP.text = "Player: " + playerString;
    }

    public void FinishPlayerString()
    {
        if (playerString == taskString)
        {
            Debug.Log("Score!");
            taskString = CreateNewTask(5);
            taskStringTMP.text = "Task: " + taskString;
        }
        else
        {
            Debug.Log("Try again!");
        }
        ResetPlayerString();
    }

    private string CreateNewTask(int digitsNum)
    {
        string res = "";
        for (int i = 0; i < digitsNum; i++)
        {
            res += Random.Range(0,10).ToString();
        }
        return res;
    }
}
