using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    public AudioClip[] clipDTMF;
    public AudioClip clipSuccess;
    public AudioClip clipFailure;
    public AudioClip clipClear;
    // public float volume = 0.7f;
    public float durationShort = 0.2f;
    public float durationLong = 1.0f;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayDTMF(string thisKey)
    {
        // Debug.Log("BEEP "+thisKey);

        switch (thisKey)
        {
            case "-1":
                // audioSource.PlayOneShot(clipDTMF[clipDTMF.Length-1], volume);
                // audioSource.clip = clipDTMF[clipDTMF.Length-1];
                audioSource.clip = clipClear;
                audioSource.Play();
                break;
            case "10":
                // audioSource.PlayOneShot(clipDTMF[clipDTMF.Length-2], volume);
                audioSource.clip = clipDTMF[clipDTMF.Length-2];
                audioSource.Play();
                break;
            default:
                int temp;
                if (int.TryParse(thisKey, out temp))
                {
                    // audioSource.PlayOneShot(clipDTMF[temp], volume);
                    audioSource.clip = clipDTMF[temp];
                    audioSource.Play();
                }
                break;
        }
        CancelInvoke("StopPlay");
        Invoke("StopPlay", durationShort);
    }

    public void PlaySuccess()
    {
        audioSource.clip = clipSuccess;
        audioSource.Play();
        CancelInvoke("StopPlay");
        Invoke("StopPlay", durationLong);
    }

    public void PlayFailure()
    {
        audioSource.clip = clipFailure;
        audioSource.Play();
        CancelInvoke("StopPlay");
        Invoke("StopPlay", durationLong);
    }

    public void StopPlay()
    {
        audioSource.Stop();
    }
}
