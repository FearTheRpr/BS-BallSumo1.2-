﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Normal.Realtime;

public class playerScoreScript : RealtimeComponent<playerScoreModel>
{ 
   private Text _playerScoreText;

    
    private void Awake()
    {
        _playerScoreText = this.gameObject.GetComponent<Text>();

    }
    protected override void OnRealtimeModelReplaced(playerScoreModel previousModel,playerScoreModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.playerScoreDidChange -= PlayerScoreDidChange;
        }
        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            { 

                model.playerScore = 0;
            }
        }

        UpdateScore();

        currentModel.playerScoreDidChange += PlayerScoreDidChange;
    }

    private void PlayerScoreDidChange(playerScoreModel model, int value)
    {
        UpdateScore();

    }
    private void UpdateScore()
    {
        _playerScoreText.text = model.playerScore.ToString();
    }
    //called from scoreboard
    public int GetScore()
    {
        return model.playerScore;
    }
    // called when ball hits goal
    public void SetScore(int points)
    {
        model.playerScore += points;
    }
}
