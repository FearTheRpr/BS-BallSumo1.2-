using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conter_rotate : MonoBehaviour
{
    //this just make sure that the player stays up right and in the center of the ball;
    public GameObject player;
    public bool zOut;
    

    void FixedUpdate()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -1.0f);
        if (player.transform.localPosition != Vector3.zero && zOut){
            player.transform.localPosition = Vector3.zero;
        }
    }
}
