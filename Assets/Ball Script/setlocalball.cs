using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class setlocalball : MonoBehaviour
{
    public RealtimeTransform RTrans;
    public GameObject selfie;
    public BallAudioManager BAM;

    private void Start()
    {
     if (RTrans.isOwnedLocallySelf)
        {
            selfie.layer = 10;
            BAM.enabled = false;
        }   
    }
}
