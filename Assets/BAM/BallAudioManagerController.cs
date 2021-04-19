using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;


public class BallAudioManagerController : RealtimeComponent<BallAudioManagerModel>
{

    public AudioSource rollAudioSource;
    public AudioSource hitAudioSource;
    
    //
    private Quaternion lastRot = Quaternion.identity;
    public AudioClip[] hitSounds;
    public AudioClip[] splashSounds;
    public float audioSpeed = 720;
    public float rollMaxVolume = 1;
    public float rollMaxPitch = 2;
    public float rollAirtimeFalloff = 0.01f;
    
    private bool isgrounded = false;
    //
    
    
    protected override void OnRealtimeModelReplaced(BallAudioManagerModel previousModel, BallAudioManagerModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.pitchDidChange -= PitchDidChange;
            previousModel.volumeDidChange -= VolumeDidChange;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                model.pitch = 0f;
                model.volume = 0f;
            }

            currentModel.volumeDidChange += VolumeDidChange;
            currentModel.pitchDidChange += PitchDidChange;
        }
    }


    public void VolumeDidChange(BallAudioManagerModel model, float v)
    {
        rollAudioSource.volume = model.volume;
    }

    public void PitchDidChange(BallAudioManagerModel model, float p)
    {
        rollAudioSource.pitch = model.pitch;
    }

    private void Update()
    {
        var rotation = this.transform.rotation;
        CalibrateRollSound(lastRot, rotation, isgrounded);
        lastRot = rotation;
    }

    void CalibrateRollSound(Quaternion LR, Quaternion CR, bool grounded)
    {
        if (grounded)
        {
            var angleDiff = Quaternion.Angle(LR, CR) / Time.deltaTime;
            float volumeMap = CustomMathFunctions.Remap(angleDiff, 0, audioSpeed, 0, rollMaxVolume);
            float pitchMap = CustomMathFunctions.Remap(angleDiff, 0, audioSpeed, 0, rollMaxPitch);
            model.volume = Mathf.Clamp(volumeMap, 0, rollMaxVolume);
            model.pitch = Mathf.Clamp(pitchMap, 0, rollMaxPitch);
        }
        else
        {
            model.volume = Mathf.Clamp((rollAudioSource.volume - rollAirtimeFalloff),0,rollMaxVolume);
            model.pitch = Mathf.Clamp((rollAudioSource.pitch + rollAirtimeFalloff),0,rollMaxPitch);
        }
    }
    
    
    
    
}
