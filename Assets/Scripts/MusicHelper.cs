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

    // Return an int 0-11 based on which note in the octave the given note is
    public static int GetLocalNote(int n) {
        return n % 12;
    }

    // Return an int 0-7 based on the natural base of the note (i.e. 2 for "B#")
    public static int GetLocalNaturalNote(int n) {
        int note = GetLocalNote(n);

        // Subtract out accidentals
        if (note >= 11) note -= 1;
        if (note >= 9) note -= 1;
        if (note >= 6) note -= 1;
        if (note >= 4) note -= 1;
        if (note >= 1) note -= 1;

        return note;
    }

    // Returns true if the given note is sharp, false if it is natural
    public static bool GetIsNoteSharp(int n) {
        int note = GetLocalNote(n);

        // If the note is an accidental
        if (note == 1 || note == 4 || note == 6 || note == 9 || note == 11) 
            return true;
        else
            return false;
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