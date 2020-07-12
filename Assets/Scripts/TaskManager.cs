using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    public TextMeshProUGUI playerStringTMP;
    public TextMeshProUGUI taskStringTMP;
    public TextMeshProUGUI menuTextTMP;
    public int maxContactsSize;
    public int minTimerCount;
    public int maxTimerCount;
    public int digitsSize;
    public int prefixSize;

    public GameObject singleContact;
    public GameObject multipleContacts;
    public GameObject panelRight;
    public Button startButton;
    public Button restartButton;
    public GameObject keyPad;

    private Dictionary<string, GameObject> myContactsDict;
    private AudioPlayer audioPlayer;
    private string playerString;
    private float timerSeconds;
    private int countSuccess;
    private int contactsSize;
    private bool gameActive;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayer>();
        gameActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive == true)
        {
            timerSeconds -= Time.deltaTime;
            if (timerSeconds <= 0)
            {
                DecreaseCounterValue();
                timerSeconds = 1;
            }
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
        GameObject temp;
        if (myContactsDict.TryGetValue(playerString, out temp))
        {
            audioPlayer.PlaySuccess();

            Destroy(myContactsDict[playerString]);
            myContactsDict.Remove(playerString);

            // Debug.Log("Score!");
            countSuccess++;

            if (myContactsDict.Count == 0)
            {
                // Debug.Log("Level up!");
                contactsSize++;
                if (contactsSize > maxContactsSize)
                {
                    // Debug.Log("Win!");
                    // Debug.Log("Successfully made " + countSuccess.ToString() + " calls!");
                    GameOver();
                }
                FillContacts();
            }
        }
        else
        {
            audioPlayer.PlayFailure();

            // Debug.Log("Try again!");
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
        GameObject temp;

        int maxIteration = 0;
        while (myContactsDict.TryGetValue(thisPrefix+newContactNum, out temp))
        {
            newContactNum = CreateContactNum(digitsSize-prefixSize);
            maxIteration++;
            if (maxIteration > 100)
            {
                break;
            }
        }

        GameObject singleContactObject = Instantiate(singleContact, transform.position, Quaternion.identity);
        singleContactObject.transform.SetParent(multipleContacts.transform,false);

        string thisCountactNum = thisPrefix+newContactNum;
        int thisTimerCount = Random.Range(minTimerCount, maxTimerCount+1);
        singleContactObject.GetComponent<SingleContact>().Setup(thisCountactNum, thisTimerCount);

        myContactsDict[thisCountactNum] = singleContactObject;
    }

    private void DisplayContacts()
    {
        taskStringTMP.text = "To call: " + "\n";

        foreach(KeyValuePair<string,GameObject> thisContact in myContactsDict)
        {
            // Debug.Log(thisContact.Key);
            // Debug.Log(thisContact.Value);
            taskStringTMP.text += thisContact.Key;
            taskStringTMP.text += "\n";
        }
    }

    public void DecreaseCounterValue()
    {
        foreach(KeyValuePair<string,GameObject> thisContact in myContactsDict)
        {
            // Debug.Log(thisContact.Key);
            // Debug.Log(thisContact.Value);
            thisContact.Value.GetComponent<SingleContact>().UpdateClock();
        }
    }

    private void FillContacts()
    {
        string prefixDigits = CreateContactNum(prefixSize);

        while (myContactsDict.Count < contactsSize)
        {
            AddNewContact(prefixDigits);
        }
    }

    public void GameOver()
    {
        // Debug.Log("Game over!");
        // Debug.Log("Successfully made " + countSuccess.ToString() + " calls!");

        // Time.timeScale = 0;
        gameActive = false;

        Image panelRightImg = panelRight.GetComponent<Image>();

        panelRightImg.color =
            new Color(
            panelRightImg.color.r,
            panelRightImg.color.g,
            panelRightImg.color.b,
            1.0f);

        keyPad.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        menuTextTMP.text = "Game over!\n";
        menuTextTMP.text += "Expert level " + contactsSize.ToString() + "/" + maxContactsSize.ToString() + "!\n";
        menuTextTMP.text += "Successfully made " + countSuccess.ToString() + " calls!";
    }

    public void StartGame()
    {
        Image panelRightImg = panelRight.GetComponent<Image>();

        panelRightImg.color =
            new Color(
            panelRightImg.color.r,
            panelRightImg.color.g,
            panelRightImg.color.b,
            0.08f);

        keyPad.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);

        playerString = "";
        timerSeconds = 1;
        countSuccess = 0;

        contactsSize = 1;

        myContactsDict = new Dictionary<string, GameObject>();
        FillContacts();

        gameActive = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
