using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Normal.Realtime;

public class playerScoreScript : RealtimeComponent<playerScoreModel>
{ 
   public Text _playerScoreText;
    public Text playerNameText;
    public color_Player CP;

    
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
        updateName();
        UpdateScore();

        currentModel.playerScoreDidChange += PlayerScoreDidChange;
    }

    private void PlayerScoreDidChange(playerScoreModel model, int value)
    {
        UpdateScore();

    }
    //updates the score on the player
    private void UpdateScore()
    {
        _playerScoreText.text = model.playerScore.ToString();
    }
    //called from scoreboard
    public int GetScore()
    {
        return model.playerScore;
    }
    // called when ball hits Water
    public void SetScore(int points)
    {
        model.playerScore += points;
    }
    public void updateName()
    {
        playerNameText.text = CP.GetName();
    }
}
