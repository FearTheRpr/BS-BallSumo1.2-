using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAMLocal : MonoBehaviour
{
    
    public AudioSource rollAudioSource;
    public AudioSource hitAudioSource;

    private Quaternion lastRot = Quaternion.identity;
    public AudioClip[] hitSounds;
    public AudioClip[] splashSounds;
    public float audioSpeed = 720;
    public float rollMaxVolume = 1;
    public float rollMaxPitch = 2;
    public float rollAirtimeFalloff = 0.01f;
    
    private bool isgrounded = true;
    
    // Update is called once per frame
    void Update()
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
            rollAudioSource.volume = Mathf.Clamp(volumeMap, 0, rollMaxVolume);
            rollAudioSource.pitch = Mathf.Clamp(pitchMap, 0, rollMaxPitch);
        }
        else
        {
            rollAudioSource.volume = Mathf.Clamp((rollAudioSource.volume - rollAirtimeFalloff), 0, rollMaxVolume);
            rollAudioSource.pitch = Mathf.Clamp((rollAudioSource.pitch + rollAirtimeFalloff), 0, rollMaxPitch);
        }

    }

    void OnCollisionEnter(Collision other) //Define sounds to play based on collision
    {
        if (other.gameObject.layer == 0) //if hitting default layer
        {
            isgrounded = true; //we probably are grounded
        } 
        
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Ball")) //if ball
        {
            hitAudioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]); //player, ball sound go
   
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            hitAudioSource.PlayOneShot(splashSounds[Random.Range(0, splashSounds.Length)]);// water, hit water sound

        } 
        
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == 0)
        {
            isgrounded = false; //if we stop touching ground we are flying
        }
    }
    
    

}