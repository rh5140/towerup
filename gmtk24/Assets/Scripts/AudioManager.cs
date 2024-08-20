using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioClip[] meows;
    [SerializeField] public AudioClip[] notes;
    private int maxRepeats = 1;
    public bool ascending = true;
    public int repeats = 0;
    public int noteIdx = 0;

    public AudioClip Meow() {
        int rand = Random.Range(0, meows.Length);
        return meows[rand];
    }

    public AudioClip Note() {
        if (repeats == maxRepeats) {
            if (ascending) {
                noteIdx += 1;
            }
            else {
                noteIdx -= 1;
            }
            repeats = 0;
        }
        if (noteIdx == notes.Length) {
            ascending = false;
            noteIdx -= 1;
        }
        if (noteIdx == -1) {
            noteIdx = 0;
            ascending = true;
        }

        repeats += 1;

        return notes[noteIdx];
    }
}
