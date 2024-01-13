using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeInit : MonoBehaviour
{
    [SerializeField] private string volumeParameter = "MasterVolume";
    [SerializeField] private AudioMixer mixer;

    private void Start()
    {
        float volumeValue = PlayerPrefs.GetFloat(volumeParameter, 0f);
        mixer.SetFloat(volumeParameter, volumeValue);
    }
}
