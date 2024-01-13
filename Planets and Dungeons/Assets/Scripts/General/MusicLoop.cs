using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicLoop : MonoBehaviour
{
    [SerializeField] private AudioSource startMusic;
    [SerializeField] private AudioSource loopMusic;
    void Start()
    {
        startMusic.Play();
        loopMusic.PlayDelayed(startMusic.clip.length);
    }
}
