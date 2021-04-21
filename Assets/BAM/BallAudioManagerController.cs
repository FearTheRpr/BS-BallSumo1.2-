using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Random = UnityEngine.Random;


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
    
    private bool isgrounded = true;
    //
    
    
    protected override void OnRealtimeModelReplaced(BallAudioManagerModel previousModel, BallAudioManagerModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.pitchDidChange -= PitchDidChange;
            previousModel.volumeDidChange -= VolumeDidChange;
            previousModel.groundedDidChange -= GroundedDidChange;
            previousModel.hitTypeDidChange -= PlayOneShotSound;
        }

        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                model.pitch = 1f;
                model.volume = 1f;
                model.hitType = 0;
            }

            currentModel.volumeDidChange += VolumeDidChange;
            currentModel.pitchDidChange += PitchDidChange;
            currentModel.hitTypeDidChange += PlayOneShotSound;
        }
    }


    public void VolumeDidChange(BallAudioManagerModel Model, float v)
    {
        rollAudioSource.volume = Model.volume;
    }

    public void PitchDidChange(BallAudioManagerModel Model, float p)
    {
        rollAudioSource.pitch = Model.pitch;
    }

    public void PlayOneShotSound(BallAudioManagerModel Model, int hitType)
    {
        Debug.Log("playoneshotsound: ");
        if (hitType == 1)
        {
            hitAudioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
            model.hitType = 0;
            Debug.Log("Play hit sound");
        }
        if(hitType == 2)
        {
            hitAudioSource.PlayOneShot(splashSounds[Random.Range(0, hitSounds.Length)]);
            model.hitType = 0;
            Debug.Log("Play water sound");
        }


    }

    public void GroundedDidChange(BallAudioManagerModel Model, bool g)
    {
        
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
    
    void OnCollisionEnter(Collision other) //Define sounds to play based on collision
    {
        if (other.gameObject.layer == 0) //if hitting default layer
        {
            model.grounded = true; //we probably are grounded
        } 
        
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Ball")) //if ball
        {
            model.hitType = 1; //false = hit player, play player hit sound
            Debug.Log(model.hitType);
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            model.hitType = 2; //true = hit probably water, hit water sound
            Debug.Log(model.hitType);
        } 
        
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 0)
        {
            model.grounded = false; //if we stop touching ground we are flying
        }
    }
    
    
}
