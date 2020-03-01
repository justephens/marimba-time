using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static MusicHelper;

public class Application : MonoBehaviour
{
    enum GameMode {
        MENU,
        NOTE_DRILL,
    };

    // Singleton
    private static Application _instance;
    public static Application GetInstance() {
        return _instance;
    }



    /////////////////
    // APPLICATION //
    /////////////////
    static GameMode gameMode = GameMode.NOTE_DRILL;     // Current game mode
    MusicDisplay display;                               // Reference to Music Display

    void Start()
    {
        _instance = this;
        display = GameObject.Find("MusicDisplay").GetComponent<MusicDisplay>();

        Drill_NextNote();
    }

    void Update()
    {
        if (gameMode == GameMode.NOTE_DRILL) {
        }
    }



    ////////////////
    // NOTE DRILL //
    ////////////////
    int drill_pitch = -1;               // Current pitch to hit
    int drill_score = 0;               // Current score
    float drill_delay = 5.0f;           // Length of time to hit note
    float drill_time = -1f;             // Time previous note began

    // Generates the next note in the Drill exercise
    void Drill_NextNote()
    {
        // Generate a random pitch and draw it to the display
        drill_pitch = Random.Range(48, 60);
        display.ClearDisplay();
        display.DisplayNote(0, drill_pitch, true);

        // Activate the note, so that it will highlight after a delay
        MarimbaNote note = MarimbaNote.GetNote(drill_pitch);
        note.ActivateNote(5f, 2.5f);

        // Reset time
        drill_time = Time.time;

        Debug.Log("Generated pitch " + drill_pitch);
        Debug.Log("Found object " + note.gameObject.name);
    }



    // Allows Marimba notes to alert the Application about collisions. Used for
    // menu selection and game playing
    public void RegisterNoteHit(GameObject note, Collision col)
    {
        Debug.Log(note.name + " hit by mallets");

        // If the user is playing the Note Drill game mode
        if (gameMode == GameMode.NOTE_DRILL)
        {
            // Correct Note
            if (note.name == GetNoteName(drill_pitch)) {
                // Increase the score based on speed
                drill_score += (int) Mathf.Max(
                    (drill_delay*1000) - Mathf.Floor((Time.time - drill_time)*100) * 10, 0);
                Debug.Log("SCORE: " + drill_score);

                // Deactivate previous note, generate a new one
                note.GetComponent<MarimbaNote>().DeactivateNote();
                Drill_NextNote();
            }
            // Incorrect Note
            else {
                drill_score -= 200;
            }
        }
    }
}
