using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Userdata
{
    public float playerColorR;
    public float playerColorG;
    public float playerColorB;
    public float playerColorA;
    public string name;
    public Userdata(Color_Name data)
    {
        playerColorR = data.playerColor.r;
        playerColorG = data.playerColor.g;
        playerColorB = data.playerColor.b;
        playerColorA = data.playerColor.a;
        name = data.player;
    }
}
