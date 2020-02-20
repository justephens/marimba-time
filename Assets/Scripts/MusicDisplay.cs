using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static MusicHelper;

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
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    // Displays a note on the clef given the arguments
    public void DisplayNote(int xpos, int pitch, bool clef)
    {
        // Create new Whole Note
        GameObject note = Instantiate(WholeNotePref);
        note.transform.parent = transform;
        note.transform.localRotation = Quaternion.identity;

        // Set y-location based on pitch and on clef
        float ylocation = 0.0f;
        if (clef) ylocation = .0925f + (.0205f * MusicHelper.GetLocalNote(pitch));

        // Set x-location based on given parameter
        float xlocation = -.05f  * xpos;

        // Update transform to reflect the x and y values just calculated
        note.transform.localPosition = new Vector3(xlocation, ylocation, 0);
    }

    // Clears the display of all notes
    public void ClearDisplay()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Display_Note");
        foreach (GameObject obj in objs)
            Destroy(obj);
    }
}
