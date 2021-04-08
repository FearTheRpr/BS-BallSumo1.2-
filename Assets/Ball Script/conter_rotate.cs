using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conter_rotate : MonoBehaviour
{
    //this just make sure that the player stays up right.

    void FixedUpdate()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -1.0f);
    }
}
