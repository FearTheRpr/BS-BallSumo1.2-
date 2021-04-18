using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ButtonVolume : MonoBehaviour
{
    public AudioMixer GameVolume;

    public void SetLevel()
    {
        GameVolume.SetFloat("Music", Mathf.Log10( + 1 ) * 20);
    }
}
