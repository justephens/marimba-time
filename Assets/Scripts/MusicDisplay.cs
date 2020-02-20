using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDisplay : MonoBehaviour
{
    public GameObject WholeNotePref;
    public GameObject HalfNotePref;
    public GameObject QuarterNotePref;
    public GameObject EighthNotePref;
    public GameObject SixteenthNotePref;

    // Start is called before the first frame update
    void Start()
    {
        NewNoteDrillNote();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    void NewNoteDrillNote() {
        int pitch = Random.Range(0, 7);

        Debug.Log("Generated pitch " + pitch);

        DisplayNote(0, pitch, true);

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
    }

    // Displays a note on the clef given the arguments
    void DisplayNote(int xpos, int pitch, bool clef)
    {
        // Create new Whole Note
        GameObject note = Instantiate(WholeNotePref);
        note.transform.parent = transform;
        note.transform.localRotation = Quaternion.identity;

        // Set y-location based on pitch and on clef
        float ylocation = 0.0f;
        if (clef) ylocation = .0925f + (.0205f * pitch);

        // Set x-location based on given parameter
        float xlocation = -.05f  * xpos;

        note.transform.localPosition = new Vector3(xlocation, ylocation, 0);
    }
}
