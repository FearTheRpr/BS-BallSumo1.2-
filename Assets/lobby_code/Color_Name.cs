﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Color_Name : MonoBehaviour
{
    public string player;
    public Color playerColor;
    public Text Pname;
    public Image colorPreview;
    public GameObject Saved;
    private void Start()
    {
        if (Script_Boi.Load()!=null)
        {
            load();
            Pname.text = player;
            colorPreview.color = playerColor;
            Saved.SetActive(true);
        }
    }
    public void AddCharacterToPlayerName(string c) //Adds c to the room name
    {

       player += c;
        Pname.text = player;
        Saved.SetActive(false);
    }
    public void ClearPlayerName()
    {
        player = string.Empty; //Clears the player name
        Pname.text = "Named Cleared";
        Saved.SetActive(false);
    }
    public void playerColorChange(Color x)
    {
        playerColor = x;
        colorPreview.color = playerColor;
        Saved.SetActive(false);
    }
    public void load()
    {
        Userdata loaded = Script_Boi.Load();
        playerColor = new Color(loaded.playerColorR, loaded.playerColorG, loaded.playerColorB, loaded.playerColorA);
        player = loaded.name;
    }
    public void save()
    {
        Script_Boi.save(this);
        Saved.SetActive(true);
    }
}
