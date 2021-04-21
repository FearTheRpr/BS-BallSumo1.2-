using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Color_Name : MonoBehaviour
{
    //local saver for name and color
    //store player name
    public string player;
    //store color
    public Color playerColor;
    //text to show player name
    public Text Pname;
    //show the color preview
    public Image colorPreview;
    //update weither or not it is saved
    public GameObject Saved;
    // the realtime model
    public color_Player RTplayer;
    //on start
    private void Start()
    {
        //if there is a local save load it set the name and preview let player now the are saved
        if (Script_Boi.Load()!=null)
        {
            load();
            Pname.text = player;
            colorPreview.color = playerColor;
            Saved.SetActive(true);
        }

    }
    //a methode to add a charicter to the name make sure saved is turned off
    public void AddCharacterToPlayerName(string c) //Adds c to the room name
    {

       player += c;
        Pname.text = player;
        Saved.SetActive(false);
    }
    //clear the player name
    public void ClearPlayerName()
    {
        player = string.Empty; //Clears the player name
        Pname.text = "Named Cleared";
        Saved.SetActive(false);
    }
    //change the color of player
    public void playerColorChange(Color x)
    {
        playerColor = x;
        colorPreview.color = playerColor;
        Saved.SetActive(false);
    }
    //load the color and name
    public void load()
    {
        Userdata loaded = Script_Boi.Load();
        playerColor = new Color(loaded.playerColorR, loaded.playerColorG, loaded.playerColorB, loaded.playerColorA);
        player = loaded.name;
    }
    //save the data tell player its save and put it to the real time
    public void save()
    {
        Script_Boi.save(this);
        Saved.SetActive(true);
        putToRT();

    }
    //this is to set the RTplayer and if load exist put to real time
    public void findRT(color_Player y)
    {
        RTplayer = y;
        if (Script_Boi.Load() != null)
        {
            putToRT();
        }
    }
    //methode to put the real time  data in
    public void putToRT()
    {
        RTplayer.setColor(playerColor);
        RTplayer.setPlayerName(player);
    }
}
