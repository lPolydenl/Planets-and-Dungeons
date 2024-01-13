using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string volumeParameter = "MasterVolume";
    [SerializeField] private Slider slider;
    private float volumeValue;

    private const float multiplier = 20f;
    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }
    private void HandleSliderValueChanged(float value)
    {
        volumeValue = Mathf.Log10(value) * multiplier;
        mixer.SetFloat(volumeParameter, volumeValue);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, volumeValue);
    }
    private void Start()
    {
        volumeValue = PlayerPrefs.GetFloat(volumeParameter, Mathf.Log10(slider.value) * multiplier);
        slider.value = Mathf.Pow(10f, volumeValue / multiplier);
    }

}
