using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using Normal.Realtime;

public class Ball_movement : MonoBehaviour
{
    /// <summary>
    /// these are the varibles
    /// first the steam vr input
    /// Left and right ahand class to get volicity from
    /// head to track the postion
    /// to keep track of button on the left hand and right hand
    /// to wait and see the head staring postion
    /// a booleen for a thr jump cooldown
    /// the riged body for the ball
    /// the multiplication factor for volicity
    /// the multiplication factor for jump
    /// to change how long the jump cooldown is
    /// to keep track of the cool down
    /// to intrack with the model to set your volicty for othe players
    /// to get other players volicty
    /// a varible to keep track of the other players volcity
    /// </summary>
    public SteamVR_Action_Boolean Pulltoggle = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("default", "ball_pull");
    public SteamVR_Behaviour_Pose vLeft;
    public SteamVR_Behaviour_Pose vRight;
    public GameObject vHead;
    private Vector3 addT;
    public Vector3 startPlaceHead;
    public bool leftOn = false;
    public bool rightOn = false;
    private bool HeadOn = true;
    private bool justjumped = true;
    public Rigidbody ball;
    public int multi_fact;
    public int multi_fact_Head;
    public float jumpCooldown;
    private float currentCooldown = 0;
    public color_Player RTV;
    public RealtimeAvatarManager aMan;
    public int Owner;
    private Vector3 colV; 


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
             
            }
            //if not (so let go of) do this
            else
            {
                //get volicty from hand to add force
                addT = vLeft.GetVelocity();
                // set the y to zero so it does not jump
                addT.y = 0;
                //multiplie the Force to get a faster result
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
            //if left is held down do this
            if (leftOn)
            {
                //get volicty from hand to add force
                addT = vLeft.GetVelocity();
                // set the y to zero so it does not jump
                addT.y = 0;
                //multiplie the Force to get a faster result
                addT = addT * multi_fact;
                // add the Force to the ball
                ball.AddForce(addT);
       
            }

        }
        //this wil wait for the head to update befor getting the starting postion for jump
        if (HeadOn && vHead.transform.localPosition.y != 0)
        {
            startPlaceHead = vHead.transform.localPosition;
            HeadOn = false;
        }

        //if the right hand grab state has changed do this
        if (Pulltoggle.GetChanged(SteamVR_Input_Sources.RightHand))
        {
            //if it is in the held state do this
            if (Pulltoggle.GetState(SteamVR_Input_Sources.RightHand))
            {
                // turn on the right held state down local var
                rightOn = true;
               
            }
            //if not (so let go of) do this
            else
            {
                //get volicty from hand to add force
                addT = vRight.GetVelocity();
                // set the y to zero so it does not jump
                addT.y = 0;
                //multiplie the Force to get a faster result
                addT = addT * multi_fact;
                // add the force to the ball
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

                //get volicty from hand to add force
                addT = vRight.GetVelocity();
                // set the y to zero so it does not jump
                addT.y = 0;
                //multiplie the Force to get a faster result
                addT = addT * multi_fact;
                // add the force to the ball
                ball.AddForce(addT);
            }
        }
        //if the head is lower then a wierd number that i got off of some random fitnse sight of how high should a squat be for a certain hight and did some wierd alibra and rounded down.
        if (vHead.transform.localPosition.y <= startPlaceHead.y / 1.3f && justjumped)
        {
            //ad instent volicty to ball
            ball.AddForce(Vector3.up*multi_fact_Head, ForceMode.VelocityChange);
            //get the jump cool down activated
            justjumped = false;
            currentCooldown = jumpCooldown;
        }
        //if jump is on cool down
        else if(!justjumped)
        {
            //decrease the cooldown time
            currentCooldown -= Time.deltaTime;
            //if cooldowns done activate the jump bool
            if (currentCooldown <=0)
            {
                justjumped = true;
            }

        }
        //set the model volicity in normcore if its there 
        if (RTV !=null && RTV.getVelocityofP()!=ball.velocity)
        {
            RTV.setVelocityofP(ball.velocity);
        }
    }

    //on collition with another ball
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //get the owner an its volicity and add it to your own vollocity
            Owner = collision.gameObject.GetComponent<RealtimeTransform>().ownerIDInHierarchy;
            colV = aMan.avatars[Owner].gameObject.GetComponentInChildren<color_Player>().getVelocityofP();
            colV = colV *1.2f;
            ball.AddForce(colV, ForceMode.VelocityChange);
        }
    }
    //a method to set the local realtime volicty controller 
    public void setRTV(color_Player x)
    {
        RTV = x;
    }
    
}
