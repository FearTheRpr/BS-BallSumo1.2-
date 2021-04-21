using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class goalScoring : MonoBehaviour
{
    public GameObject scoreBoardContainer;
    public GameObject playerNamer;
    private scoreScript scoreBoardScript;
    private int ownerID = -1;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoardScript = scoreBoardContainer.GetComponentInChildren<scoreScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //checks to see if its ball hitting the goal
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Ball has hit");
            //checks the owner ID of ball (NEEDS TO CHANGE IN ORDER TO INCORPERATE THE BALL THAT THE PLAYER IS ATTACHED TO)
            //ownerID = collision.gameObject.GetComponent<RealtimeTransform>().ownerIDInHierarchy;
           
            ownerID = playerNamer.GetComponent<Ball_movement>().Owner;
            if (ownerID != -1)
            {
                scoreBoardScript.SetScoreForPlayer(ownerID, 1);
            }
        }
    }
}
