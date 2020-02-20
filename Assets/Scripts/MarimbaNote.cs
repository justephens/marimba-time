using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarimbaNote : MonoBehaviour
{
    public static Dictionary<string, MarimbaNote> notes = new Dictionary<string, MarimbaNote>();

    public AudioClip soundToPlay;
    public bool useVelocityBasedVolume = true;
    public float velocityVolumeMultiplier = 5.0f;

    private bool note_active = false;
    private float activate_time = 0f;
    private float hl_delay_time = 0f;
    private float hl_ramp_time = 0f;

    void Start()
    {
        // Add this note block to the global list of notes
        notes.Add(gameObject.name, this);
    }

    void Update()
    {
        if (note_active) {
            // Get the highlight color from the Highlight material in the renderer
            MeshRenderer rend = GetComponent<MeshRenderer>();
            Color tint = rend.materials[1].GetColor("_TintColor");

            // Linear interpolate between transparent and visible highlight colors,
            // based on time since "activation"
            tint = Color.Lerp(
                new Color(tint.r, tint.g, tint.b, 0.0f),
                new Color(tint.r, tint.g, tint.b, 0.5f),
                Mathf.Clamp((Time.time - activate_time - hl_delay_time) / hl_ramp_time, 0, 1));

            // Set the highlight color to the Lerp'ed color
            rend.materials[1].SetColor("_TintColor", tint);
        }
    }

    public void ActivateNote(float hl_delay, float hl_ramp)
    {
        note_active = true;
        activate_time = Time.time;
        hl_delay_time = hl_delay;
        hl_ramp_time = hl_ramp;
    }

    public void UnHighlight()
    {
        MeshRenderer rend = GetComponent<MeshRenderer>();
        Color tint = rend.materials[1].GetColor("_TintColor");
        tint = new Color(tint.r, tint.g, tint.b, 0);
        rend.materials[1].SetColor("_TintColor", tint);
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



        // Let the application manager know about the collision
        //Application.RegisterNoteHit(gameObject, col);
    }
}
