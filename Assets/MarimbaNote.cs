using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarimbaNote : MonoBehaviour
{
    public static Dictionary<string, MarimbaNote> notes;

    public AudioClip soundToPlay;
    public bool useVelocityBasedVolume = true;
    public float velocityVolumeMultiplier = 5.0f;


    void Start()
    {
        // Add this note block to the global list of notes
        notes.Add(this.gameObject.name, this);
    }


    // This runs every time the block is hit
    void OnTriggerEnter(Collider col)
    {
        // Audio source to play from
        AudioSource audio = null;

        // Look through existing sources for an idle AudioSource
        AudioSource[] sources = this.GetComponents<AudioSource>();
        foreach (AudioSource a in sources) {
            if (!a.isPlaying) {
                audio = a;
                break;
            }
        }

        // If no idle source can be found, create one
        if (audio == null) {
            audio = this.gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        }

        // Set properties of AudioSource, then play clip
        audio.clip = soundToPlay;
        audio.volume = (useVelocityBasedVolume) ? col.attachedRigidbody.velocity.magnitude * velocityVolumeMultiplier : 1.0f;
        audio.Play();
    }
}
