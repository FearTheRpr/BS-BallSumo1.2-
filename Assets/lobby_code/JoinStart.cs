using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
/// <summary>
/// To use:
///
///This is what starts and exits lobbies!
///
/// Connect StartRoom to a button to start, leaveRoom to leave
///
/// There should only be one Roomname and Realtime in the scene!
/// 
/// </summary>
public class JoinStart : MonoBehaviour
{
  
    private Realtime rt;
    private RoomName rn;

    public void Start() //Finds and sets realtime and roomname, requires only one in a scene.
    {
        rt = FindObjectOfType<Realtime>(); 
        rn = FindObjectOfType<RoomName>();
    }

    public void startRoom()
    {
        rt.Connect(rn.roomName.ToString());
    }

    public void leaveRoom()
    {
        rt.Disconnect();
    }
}
