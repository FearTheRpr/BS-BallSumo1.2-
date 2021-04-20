using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Adds characters or clears the room name
/// To use:
///
/// Put this code on a Steamvr activator such as a button
/// Set RN to a RoomName in the scene, there should only be one in the scene!
/// Connect one of the functions to the on pressed/clicked event, set the public field to the character you want if it's AddCharacterToString
///
/// When the character presses a button, it will either input the string you want, or clear the room name, depending on what function.
/// 
/// </summary>
public class RoomInput : MonoBehaviour
{
    

    public RoomName rn; //roomName.cs to change
    public Text Room; // to show current room name
    
    public void AddCharacterToRoomName(string c) //Adds c to the room name and update text
    {

        rn.roomName += c;
        Room.text = rn.roomName;
    }

    public void ClearRoomName()
    {
        rn.roomName = String.Empty; //Clears the room name
        Room.text = "Cleared";
    }
    
    

}
