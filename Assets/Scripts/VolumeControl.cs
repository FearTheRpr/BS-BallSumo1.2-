using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer GameVolume;
    //allows a slider to change the volume logarithmically so it changes 1:1
    public void SetLevel(float sliderValue)
    {
        GameVolume.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }
}
