using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static MusicHelper;

public class MarimbaNote : MonoBehaviour
{
    public AudioClip soundToPlay;

    // TODO: Implement velocity based volume
    public bool useVelocityBasedVolume = true;
    public float velocityVolumeMultiplier = 5.0f;

    private bool note_active = false;
    private float activate_time = -1f;
    private float hl_delay_time = 0f;
    private float hl_ramp_time = 0f;

    // Returns the given note
    public static MarimbaNote GetNote(string name) {
        GameObject note = GameObject.Find(name);
        return note.GetComponent<MarimbaNote>();
    }
    public static MarimbaNote GetNote(int n) {
        return GetNote(MusicHelper.GetNoteName(n));
    }

    void Start()
    {
    }

    void Update()
    {
        // If the note is "active", i.e. awaiting the user to hit it, then highlight
        // using the parameters set at the point of activation
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

        // If the note is not active, then we remove highlight from it
        else {
            MeshRenderer rend = GetComponent<MeshRenderer>();
            Color tint = rend.materials[1].GetColor("_TintColor");
            tint = new Color(tint.r, tint.g, tint.b, 0);
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

    public void DeactivateNote()
    {
        note_active = false;
        activate_time = -1f;
        hl_delay_time = 0f;
        hl_ramp_time = 0;
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

        // Mallet volume based on velocity
        if (col.gameObject.tag == "Marimba_Mallet")
            audio.volume = (useVelocityBasedVolume) ? 1.0f * velocityVolumeMultiplier : 1.0f;
        // If not a mallet hitting the note, play quietly
        else audio.volume = 0.1f;

        // Set properties of AudioSource, then play clip
        audio.clip = soundToPlay;
        audio.Play();

        // Let the application manager know about the collision
        Application.GetInstance().RegisterNoteHit(gameObject, col);
    }
}
