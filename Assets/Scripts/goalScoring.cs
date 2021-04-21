using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class goalScoring : MonoBehaviour
{
    public GameObject scoreBoardContainer;
    public GameObject playerNamer;
    public scoreScript scoreBoardScript;
    private int ownerID;

    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collisions)
    {
        //checks to see if its ball hitting the goal
        if (collisions.gameObject.CompareTag("Ball"))
        { 
           
            ownerID = collisions.gameObject.GetComponent<Ball_movement>().Owner;
            if (ownerID != -1)
            {
                
                scoreBoardScript.SetScoreForPlayer(ownerID, 1);
            }
        }
    }
}
