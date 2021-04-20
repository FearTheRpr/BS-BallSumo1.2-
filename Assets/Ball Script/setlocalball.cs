using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class setlocalball : MonoBehaviour
{
    //Real tim transform to see the owner
    public RealtimeTransform RTrans;
    // the game object of self to set layer
    public GameObject selfie;
    //the color player for the volicity setter
    public color_Player CP;

    private void Start()
    {
        // if it is owner is local then set the layer give the color name operating previltio to set to the avatar abd ball movemen to set moderl volocity
     if (RTrans.isOwnedLocallySelf)
        {
            selfie.layer = 10;
            FindObjectOfType<Color_Name>().findRT(CP);
            FindObjectOfType<Ball_movement>().setRTV(CP);
        }   
     
    }
}
