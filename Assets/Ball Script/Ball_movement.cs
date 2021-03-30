using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Ball_movement : MonoBehaviour
{
    public SteamVR_Action_Boolean Pulltoggle = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "ball_pull");
    public GameObject leftHand;
    public GameObject rightHand;
    public Vector3 leftStart;
    public Vector3 rightStart;
    private Vector3 addT;
    public bool leftOn = false;
    public bool rightOn = false;
    public Rigidbody ball;
    private void FixedUpdate()
    {
        if (Pulltoggle.GetChanged(SteamVR_Input_Sources.LeftHand)) 
        {
            if (Pulltoggle.GetState(SteamVR_Input_Sources.LeftHand))
            {
                leftOn = true;
                leftStart =  leftHand.transform.localPosition;
            }
            else
            {
                addT = leftStart - leftHand.transform.localPosition;
                ball.AddTorque(addT);
                addT = addT * 1000;
                leftOn = false;
            }
        }
        else
        {
            if (leftOn)
            {
                addT = leftStart - leftHand.transform.localPosition;
                addT = addT * 1000;
                ball.AddTorque(addT);
                leftStart = leftHand.transform.localPosition;
            }
        }
        if (Pulltoggle.GetChanged(SteamVR_Input_Sources.RightHand))
        {
            if (Pulltoggle.GetState(SteamVR_Input_Sources.RightHand))
            {
                rightOn = true;
                rightStart = rightHand.transform.localPosition;
            }
            else
            {
                addT = rightStart - rightHand.transform.localPosition;
                addT = addT * 1000;
                ball.AddTorque(addT);
                rightOn = false;
            }
        }
        else
        {
            if (rightOn)
            {
                addT = rightStart - rightHand.transform.localPosition;
                addT = addT * 1000;
                ball.AddTorque(addT);
                rightStart = rightHand.transform.position;
            }
        }
    }

}
