using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SoundScript
{
    public enum AudioTypes { soundEffect, music }
    public AudioTypes audioType;
    
    [HideInInspector] public AudioSource audioSource;
    public string clipName;
    public AudioClip audioClip;
    public bool isLoop;
    public bool playOnAwake;

    [Range(0f, 1f)] 
    public float volume = 0.5f;
}
