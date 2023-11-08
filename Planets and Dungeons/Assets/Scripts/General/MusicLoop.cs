using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicLoop : MonoBehaviour
{
    [SerializeField] private AudioClip engineStartClip;
    [SerializeField] private AudioClip engineLoopClip;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(playEngineSound());
    }

    IEnumerator playEngineSound()
    {
        audioSource.clip = engineStartClip;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.clip = engineLoopClip;
        audioSource.Play();
    }
}
