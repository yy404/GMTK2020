using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public TextMeshProUGUI playerStringTMP;
    public TextMeshProUGUI taskStringTMP;
    public int contactsSize;
    public int digitsSize;
    public int prefixSize;
    public int currentCounterValue;

    private Dictionary<string, int> myContactsDict;
    private AudioPlayer audioPlayer;
    private string playerString;
    private float timerSeconds;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayer>();

        playerString = "";
        timerSeconds = 1;

        myContactsDict = new Dictionary<string, int>();
        string prefixDigits = CreateContactNum(prefixSize);
        for (int i = 0; i < contactsSize; i++)
        {
            AddNewContact(prefixDigits);
        }
        DisplayContacts();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCounterValue > 0)
        {
            timerSeconds -= Time.deltaTime;
            if (timerSeconds <= 0)
            {
                DecreaseCounterValue();
                timerSeconds = 1;
            }
        }
        else
        {
            Debug.Log("Game over!");
        }
    }

    public void AddToPlayerString(string thisString)
    {
        playerString += thisString;
        // Debug.Log(playerString);
        playerStringTMP.text = "Calling: " + playerString;
    }

    public void ResetPlayerString()
    {
        playerString = "";
        playerStringTMP.text = "Calling: " + playerString;
    }

    public void FinishPlayerString()
    {
        int temp;
        if (myContactsDict.TryGetValue(playerString, out temp))
        {
            audioPlayer.PlaySuccess();

            myContactsDict.Remove(playerString);

            Debug.Log("Score!");
            // AddNewContact();
            DisplayContacts();

            if (myContactsDict.Count == 0)
            {
                Debug.Log("Win!");
            }
        }
        else
        {
            audioPlayer.PlayFailure();

            Debug.Log("Try again!");
        }
        ResetPlayerString();
    }

    private string CreateContactNum(int digitsNum)
    {
        string res = "";
        for (int i = 0; i < digitsNum; i++)
        {
            res += Random.Range(0,10).ToString();
        }
        return res;
    }

    private void AddNewContact(string thisPrefix)
    {
        string newContactNum = CreateContactNum(digitsSize-prefixSize);
        int temp;
        while (myContactsDict.TryGetValue(thisPrefix+newContactNum, out temp))
        {
            newContactNum = CreateContactNum(digitsSize-prefixSize);
        }
        myContactsDict[thisPrefix+newContactNum] = 1;
    }

    private void DisplayContacts()
    {
        taskStringTMP.text = "To call: " + "\n";

        foreach(KeyValuePair<string,int> thisContact in myContactsDict)
        {
            // Debug.Log(thisContact.Key);
            // Debug.Log(thisContact.Value);
            taskStringTMP.text += thisContact.Key;
            taskStringTMP.text += "\n";
        }
    }

    public void DecreaseCounterValue()
    {
        currentCounterValue--;
        Debug.Log(currentCounterValue);
    }
}
