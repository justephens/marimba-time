using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarimbaNote : MonoBehaviour
{
    public static Dictionary<string, MarimbaNote> notes = new Dictionary<string, MarimbaNote>();

    public AudioClip soundToPlay;
    public bool useVelocityBasedVolume = true;
    public float velocityVolumeMultiplier = 5.0f;

    private MeshRenderer rend;
    private Material baseMaterial;
    public Material highlightMaterial;

    void Start()
    {
        // Add this note block to the global list of notes
        notes.Add(gameObject.name, this);

        rend = GetComponent<MeshRenderer>();
        baseMaterial = rend.material;
    }

    void Highlight()
    {
        rend.material = highlightMaterial;
    }

    void UnHighlight()
    {
        rend.material = baseMaterial;
    }


    // This runs every time the block is hit
    void OnCollisionEnter(Collision col)
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
        audio.volume = (useVelocityBasedVolume) ? col.relativeVelocity.magnitude * velocityVolumeMultiplier : 1.0f;
        audio.Play();
    }
}
