using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyValue;
    private TaskManager taskManager;

    // Start is called before the first frame update
    void Start()
    {
        taskManager = GameObject.FindWithTag("TaskManager").GetComponent<TaskManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        // Debug.Log(keyValue);
        if (keyValue == "-1")
        {
            taskManager.ResetPlayerString();
        }
        else if (keyValue == "10")
        {
            taskManager.FinishPlayerString();
        }
        else
        {
            taskManager.AddToPlayerString(keyValue);
        }
    }
}
