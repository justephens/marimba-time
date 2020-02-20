using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Application : MonoBehaviour
{
    enum GameMode {
        MAIN,
        NOTE_DRILL,
    };
    static GameMode gameMode = GameMode.NOTE_DRILL;

    MusicDisplay display;

    // Start is called before the first frame update
    void Start()
    {
        display = GameObject.Find("MusicDisplay").GetComponent<MusicDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (gameMode == GameMode.NOTE_DRILL) {
            int pitch = Random.Range(0, 7);

            Debug.Log("Generated pitch " + pitch);

            display.DisplayNote(0, pitch, true);

            GameObject marimbaBlock;
            switch(pitch) {
            case 0:
                marimbaBlock = GameObject.Find("C2");
                break;
            case 1:
                marimbaBlock = GameObject.Find("D2");
                break;
            case 2:
                marimbaBlock = GameObject.Find("E2");
                break;
            case 3:
                marimbaBlock = GameObject.Find("F2");
                break;
            case 4:
                marimbaBlock = GameObject.Find("G2");
                break;
            case 5:
                marimbaBlock = GameObject.Find("A3");
                break;
            case 6:
                marimbaBlock = GameObject.Find("B3");
                break;
            case 7:
            default:
                marimbaBlock = GameObject.Find("C3");
                break;
            }

            Debug.Log("Found object " + marimbaBlock.name);

            marimbaBlock.GetComponent<MarimbaNote>().ActivateNote(5f, 5f);
        }*/
    }

    // Allows Marimba notes to alert the Application about collisions. Used for
    // menu selection and game playing
    public void RegisterNoteHit(GameObject note, Collision col)
    {
        Debug.Log(note.name + " hit by mallets");
    }
}
