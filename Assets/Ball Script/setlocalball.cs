using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class setlocalball : MonoBehaviour
{
    public RealtimeTransform RTrans;
    public GameObject selfie;
    public color_Player CP;
  
    private void Start()
    {
     if (RTrans.isOwnedLocallySelf)
        {
            selfie.layer = 10;
            GetComponent<AudioSource>().enabled = false;
            FindObjectOfType<Ball_movement>().setRTV(CP);
            FindObjectOfType<Color_Name>().findRT(CP);
        }   
    }
}
