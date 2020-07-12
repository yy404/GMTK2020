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

    private Dictionary<string, int> myContactsDict;
    private string playerString;
    private AudioPlayer audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayer>();

        playerString = "";

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
        taskStringTMP.text = "Contacts: " + "\n";

        foreach(KeyValuePair<string,int> thisContact in myContactsDict)
        {
            // Debug.Log(thisContact.Key);
            // Debug.Log(thisContact.Value);
            taskStringTMP.text += thisContact.Key;
            taskStringTMP.text += "\n";
        }
    }
}
