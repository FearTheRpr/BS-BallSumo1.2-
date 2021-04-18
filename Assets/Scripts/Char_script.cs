using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Char_script : MonoBehaviour
{
    public Text input;
    public RoomInput room;
    public Color_Name playInfo;
    public Image ColorIn;
    private string In;

    private void Awake()
    {
        if (In !=null) 
        {
            In = input.text;
        }
    }
    public void InCharRoom()
    {
        room.AddCharacterToRoomName(In);
    }
    public void InCharName()
    {
        playInfo.AddCharacterToPlayerName(In);
    }
    public void color_select()
    {
        playInfo.playerColorChange(ColorIn.color);
    }
}
