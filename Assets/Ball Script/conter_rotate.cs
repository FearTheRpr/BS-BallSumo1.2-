using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conter_rotate : MonoBehaviour
{

    void FixedUpdate()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, this.transform.parent.rotation.z * -1.0f);
    }
}
