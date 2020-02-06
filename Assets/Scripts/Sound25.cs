using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Sound25 : MonoBehaviour
{

    //-----------[Variables]-------------//
    public AudioClip soundToPlay;
    public float volume;
    public bool played = false;
    //-----------------------------------//


    void Start()
    {
        this.GetComponent<AudioSource>().clip = soundToPlay;
        this.GetComponent<AudioSource>().volume = volume;
    }

    // Make sure  your camera or your camera's parent has a collider...
    // Make sure Box collider has the trigger check marked on ckecked...
    // MAke Sure This Script is attached to the Cube only...
    // Make sure to take frequent brakes when programming. in helps calm you to make you think better..

    void OnTriggerEnter()
    {
        if (!played)
        {
            this.GetComponent<AudioSource>().Play();
            played = false;
        }
    }
}
