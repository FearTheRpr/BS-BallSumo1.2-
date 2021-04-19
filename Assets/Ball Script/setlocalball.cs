using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class setlocalball : MonoBehaviour
{
    public RealtimeTransform RTrans;
    public GameObject selfie;
    public Color_Name CM;

    private void Start()
    {
     if (RTrans.isOwnedLocallySelf)
        {
            selfie.layer = 10;
            CM = FindObjectOfType<Color_Name>();
            CM.findRT();
        }   
     
    }
}
