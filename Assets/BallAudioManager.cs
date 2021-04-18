using System;
using System.Collections;
using System.Collections.Generic;
using Normal.Realtime;
using Normal.Realtime.Native;
using UnityEngine;
using Random = UnityEngine.Random;


public class BallAudioManager : MonoBehaviour
{
    public AudioSource rollAud;
    public AudioOutput AudOut;
    public AudioSource hitAud;
    private Quaternion lastRot = Quaternion.identity;
    public AudioClip[] hitSounds;
    public AudioClip[] splashSounds;
    public float audioSpeed = 720;
    public float rollMaxVolume = 1;
    public float rollMaxPitch = 2;
    public float rollAirtimeFalloff = 0.01f;
    
    private bool isgrounded = false;

    
    void Update()
    { 
        var rotation = this.transform.rotation;

        CalibrateRollSound(lastRot, rotation, isgrounded);
        lastRot = rotation;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 0)
        {
            isgrounded = true;
        } else{
            if (other.gameObject.layer == LayerMask.NameToLayer("Ball"))
            {
                hitAud.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
            } 
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 0)
        {
            isgrounded = false;
        }
    }
    void CalibrateRollSound(Quaternion LR, Quaternion CR, bool grounded)
    {
        if (grounded)
        {
            var angleDiff = Quaternion.Angle(LR, CR) / Time.deltaTime;
            float volumeMap = CustomMathFunctions.Remap(angleDiff, 0, audioSpeed, 0, rollMaxVolume);
            float pitchMap = CustomMathFunctions.Remap(angleDiff, 0, audioSpeed, 0, rollMaxPitch);
            rollAud.volume = Mathf.Clamp(volumeMap, 0, rollMaxVolume);
            rollAud.pitch = Mathf.Clamp(pitchMap, 0, rollMaxPitch);
        }
        else
        {
            rollAud.volume = Mathf.Clamp((rollAud.volume - rollAirtimeFalloff),0,rollMaxVolume);
            rollAud.pitch= Mathf.Clamp((rollAud.pitch + rollAirtimeFalloff),0,rollMaxPitch);
        }
    }

}