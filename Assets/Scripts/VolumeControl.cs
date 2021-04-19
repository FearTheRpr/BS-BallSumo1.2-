using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer GameVolume;

    public void SetLevel(float sliderValue)
    {
        GameVolume.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }
}
