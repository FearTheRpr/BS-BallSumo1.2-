using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class goalScoring : MonoBehaviour
{
    public GameObject scoreBoardContainer;
    public GameObject playerNamer;
    public scoreScript scoreBoardScript;
    private int ownerID = -1;

    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        //checks to see if its ball hitting the goal
        if (collision.gameObject.CompareTag("Water"))
        { 
           
            ownerID = playerNamer.GetComponent<Ball_movement>().Owner;
            if (ownerID != -1)
            {
                
                scoreBoardScript.SetScoreForPlayer(ownerID, 1);
            }
        }
    }
}
