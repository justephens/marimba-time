using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Throughout the application, notes are stored as an integer, counting its
// position on the keyboard above A0 (i.e. 0 = A0, 12 = A1, 13 = A1#/B1b, etc.)
//
// This file contains helper functions to help manipulate these values
public static class MusicHelper
{
    private static readonly char[] local_note_lookup = {'A', 'A', 'B', 'C', 'C', 'D', 'D', 'E', 'F', 'F', 'G', 'G'};

    // Return which octave a note is in
    public static int GetOctave(int n) {
        return n / 12;
    }

    // Return an int 1-12 based on which note in the octave the given note is
    public static int GetLocalNote(int n) {
        return n % 12;
    }

    // Gets the full note name of a note
    public static string GetNoteName(int n) {
        int octave = GetOctave(n);
        int note = GetLocalNote(n);
        string name = local_note_lookup[note].ToString() + octave.ToString();

        // If the note is an accidental, add a sharp then append the conjugate name
        if (note == 1 || note == 4 || note == 6 || note == 9 || note == 11) {
            name += "#";
        }

        return name;
    }
}