using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Ball_movement : MonoBehaviour
{
    /// <summary>
    /// these are the varibles
    /// first the steam vr input
    /// left hand game object to take postion from
    /// same but for the right
    /// to keep track of the postion of the right hand
    /// same but for the left
    /// a vector 3 to keep the math done to the ball
    /// to keep track if the left hand grab is held
    /// to keep track if the right hand grab is held
    /// the riged body for the ball
    /// </summary>
    public SteamVR_Action_Boolean Pulltoggle = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "ball_pull");
    public GameObject leftHand;
    public GameObject rightHand;
    public Vector3 leftStart;
    public Vector3 rightStart;
    private Vector3 addT;
    public bool leftOn = false;
    public bool rightOn = false;
    public Rigidbody ball;
    public GameObject play;
  /*  private void Start()
    {
        ball = this.GetComponent<Rigidbody>();
        leftHand = GameObject.FindGameObjectWithTag("LeHand");
        rightHand = GameObject.FindGameObjectWithTag("RiHand");
    }*/
    //fixed update because there is physic involved
    private void FixedUpdate()
    {
        //if the left hand grab state has changed do this
        if (Pulltoggle.GetChanged(SteamVR_Input_Sources.LeftHand)) 
        {
            //if it is in the held state do this
            if (Pulltoggle.GetState(SteamVR_Input_Sources.LeftHand))
            {
                // turn on the left held state down local var
                leftOn = true;
                //keep track of the starting local position
                leftStart =  leftHand.transform.localPosition;
            }
            //if not (so let go of) do this
            else
            {
                //subtrackt the left hand start in relation to where it is now
                addT = leftStart - leftHand.transform.localPosition;
                //multiplie the toque to exmise the results (make puplic var for stream lineing process)
                addT = addT * 1000;
                // add the torque to the ball
                ball.AddTorque(addT);
                //turn the grab state off
                leftOn = false;
            }
        }
        //if no state change for left hand do this
        else
        {
            //if right is held down do this
            if (leftOn)
            {
                //subtrackt the left hand start in relation to where it is now
                addT = leftStart - leftHand.transform.localPosition;
                //multiplie the toque to exmise the results (make puplic var for stream lineing process)
                addT = addT * 1000;
                // add the torque to the ball
                ball.AddTorque(addT);
                //keep track of the starting local position
                leftStart = leftHand.transform.localPosition;
            }
        }
        //if the right hand grab state has changed do this
        if (Pulltoggle.GetChanged(SteamVR_Input_Sources.RightHand))
        {
            //if it is in the held state do this
            if (Pulltoggle.GetState(SteamVR_Input_Sources.RightHand))
            {
                // turn on the right held state down local var
                rightOn = true;
                //keep track of the starting local position
                rightStart = rightHand.transform.localPosition;
            }
            //if not (so let go of) do this
            else
            {
                //subtrackt the right hand start in relation to where it is now
                addT = rightStart - rightHand.transform.localPosition;
                //multiplie the toque to exmise the results (make puplic var for stream lineing process)
                addT = addT * 1000;
                // add the torque to the ball
                ball.AddTorque(addT);
                //turn the grab state off
                rightOn = false;
            }
        }
        //if no state change for right hand do this do this
        else
        {
            //if right is held down do this
            if (rightOn)
            {
                //subtrackt the right hand start in relation to where it is now
                addT = rightStart - rightHand.transform.localPosition;
                //multiplie the toque to exmise the results (make puplic var for stream lineing process)
                addT = addT * 1000;
                // add the torque to the ball
                ball.AddTorque(addT);
                //keep track of the starting local position
                rightStart = rightHand.transform.position;
            }
        }
    }
}
