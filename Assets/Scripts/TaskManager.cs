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

    private AudioPlayer audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayer>();

        playerString = "";

        taskString = CreateNewTask(5);
        taskStringTMP.text = "Contacts: " + taskString;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToPlayerString(string thisString)
    {
        playerString += thisString;
        // Debug.Log(playerString);
        playerStringTMP.text = "Call: " + playerString;
    }

    public void ResetPlayerString()
    {
        playerString = "";
        playerStringTMP.text = "Call: " + playerString;
    }

    public void FinishPlayerString()
    {
        if (playerString == taskString)
        {
            audioPlayer.PlaySuccess();

            Debug.Log("Score!");
            taskString = CreateNewTask(5);
            taskStringTMP.text = "Contacts: " + taskString;
        }
        else
        {
            audioPlayer.PlayFailure();

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
