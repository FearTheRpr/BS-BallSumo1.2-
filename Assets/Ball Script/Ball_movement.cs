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
    /// the multiplication factor
    /// </summary>
    public SteamVR_Action_Boolean Pulltoggle = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "ball_pull");
    public GameObject leftHand;
    public GameObject rightHand;
    public SteamVR_Behaviour_Pose vLeft;
    public SteamVR_Behaviour_Pose vRight;
    public GameObject vHead;
    public Vector3 leftStart;
    public Vector3 rightStart;
    private Vector3 addT;
    public Vector3 startPlaceHead;
    public bool leftOn = false;
    public bool rightOn = false;
    private bool HeadOn = true;
    private bool justjumped = true;
    public Rigidbody ball;
    public GameObject play;
    public int multi_fact;
    public int multi_fact_Head;
    public float jumpCooldown;
    private float currentCooldown = 0;


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
                //addT = leftStart - leftHand.transform.localPosition;
                //multiplie the toque to exmise the results (make puplic var for stream lineing process)
                addT = vLeft.GetVelocity();
                addT.y = 0;
                addT = addT * multi_fact;
                // add the torque to the ball
                ball.AddForce(addT);
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
               //addT = leftStart - leftHand.transform.localPosition;
                addT = vLeft.GetVelocity();
                addT.y = 0;
                //multiplie the toque to exmise the results (make puplic var for stream lineing process)
                addT = addT * multi_fact;
                // add the torque to the ball
                ball.AddForce(addT);
                //keep track of the starting local position
                leftStart = leftHand.transform.localPosition;
            }
            else if (HeadOn && vHead.transform.localPosition.y != 0)
            {
                startPlaceHead = vHead.transform.localPosition;
                HeadOn = false;
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
                //addT = rightStart - rightHand.transform.localPosition;
                addT = vRight.GetVelocity();
                addT.y = 0;
                //multiplie the toque to exmise the results (make puplic var for stream lineing process)
                addT = addT * multi_fact;
                // add the torque to the ball
                ball.AddForce(addT);
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
                //addT = rightStart - rightHand.transform.localPosition;
                //multiplie the toque to exmise the results (make puplic var for stream lineing process)
                addT = vRight.GetVelocity();
                addT.y = 0;
                addT = addT * multi_fact;
                // add the torque to the ball
                ball.AddForce(addT);
                //keep track of the starting local position
                rightStart = rightHand.transform.position;
            }
        }


        if (vHead.transform.localPosition.y <= startPlaceHead.y / 1.3f && justjumped)
        {
            ball.AddForce(Vector3.up*multi_fact_Head, ForceMode.VelocityChange);

            justjumped = false;
            currentCooldown = jumpCooldown;
        }
        else if(!justjumped)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown <=0)
            {
                justjumped = true;
            }

        }
       // ball.velocity;
    }
    
}
