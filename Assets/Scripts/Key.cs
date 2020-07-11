using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyValue;
    public float clickForce = 1000;

    private TaskManager taskManager;
    private AudioPlayer audioPlayer;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        taskManager = GameObject.FindWithTag("TaskManager").GetComponent<TaskManager>();
        audioPlayer = GameObject.FindWithTag("AudioPlayer").GetComponent<AudioPlayer>();
        rb = GetComponent<Rigidbody2D>();

        clickForce = 1000;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        audioPlayer.PlayDTMF(keyValue);

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

        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;
        Vector3 moveDirection = transform.position - clickPosition;
        rb.AddForce(moveDirection * clickForce);
    }
}
