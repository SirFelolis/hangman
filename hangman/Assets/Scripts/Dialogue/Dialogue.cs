using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;

    [Range(0f, 2f)]
    public float voicePitch = 1f;

    [Range(0.001f, 0.2f)]
    public float talkSpeed = 0.05f;

    public bool pitchVariation = false;


    public AudioClip[] voiceSound;

    [TextArea(3, 10)]
    public string[] sentences;
}
