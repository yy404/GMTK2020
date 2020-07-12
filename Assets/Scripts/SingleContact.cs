using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleContact : MonoBehaviour
{
    public Text contactNumText;
    public Image sandClockImg;

    private string contactNum;
    private int currTimer;
    private int maxTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(string thisString, int timerCount)
    {
        contactNum = thisString;
        contactNumText.text = contactNum;
        maxTimer = timerCount;
        currTimer = maxTimer;
    }

    public void UpdateClock()
    {
        currTimer--;
        sandClockImg.fillAmount = (float) currTimer / (float) maxTimer;

        if (currTimer < 0)
        {
            TaskManager taskManager = FindObjectOfType<TaskManager>();
            taskManager.GameOver();
        }
    }
}
