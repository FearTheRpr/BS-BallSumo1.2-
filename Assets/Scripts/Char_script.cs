using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Char_script : MonoBehaviour
{
    //class for the keyboard
    //text to get a input
    public Text input;
    //to get the room class
    public RoomInput room;
    //get the colorname to set player stuff
    public Color_Name playInfo;
    //to get a color input 
    public Image ColorIn;
    //the input
    private string In;

    private void Awake()
    {
        //if the text exist set the input to the text
        if (input !=null) 
        {
            In = input.text;
        }
    }
    //put the input into the room code
    public void InCharRoom()
    {
        room.AddCharacterToRoomName(In);
    }
    //put the charicter into the the player name
    public void InCharName()
    {
        playInfo.AddCharacterToPlayerName(In);
    }
    //put the color in the color avatar
    public void color_select()
    {
        playInfo.playerColorChange(ColorIn.color);
    }
}
