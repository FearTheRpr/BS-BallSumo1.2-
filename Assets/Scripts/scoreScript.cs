using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEngine.UI;



public class scoreScript : RealtimeComponent<scoreModel>
{
    public Text _scoreBoardTextBox;
    public RealtimeAvatarManager _avatarManager;
    public GameObject ColorPlayerScript;

    private void OnEnable()
    {
        _avatarManager.avatarCreated += AvatarChangedUpdateScoreBoard;
        _avatarManager.avatarDestroyed += AvatarChangedUpdateScoreBoard;
    }
    
    private void AvatarChangedUpdateScoreBoard(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar)
    {
        setScoreBoardText();
        setColors();
    }

    protected override void OnRealtimeModelReplaced(scoreModel previousModel, scoreModel currentModel)
    {
        if (previousModel != null)
        {
            previousModel.scoreBoardTextDidChange -= ScoreBoardTextDidChange;
        }
        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                model.scoreBoardText = "";
            }

            UpdateScoreboard();
            currentModel.scoreBoardTextDidChange += ScoreBoardTextDidChange;
        }

       
    }

    private void ScoreBoardTextDidChange(scoreModel model, string value)
    {
        UpdateScoreboard();
    }

    private void UpdateScoreboard()
    {
        _scoreBoardTextBox.text = model.scoreBoardText;
    }

    private void setScoreBoardText()
    {
        //temp ID for players
        int playerID = 0;
        model.scoreBoardText = "";

        //loops through players to gather information to display information (name and score in this case
        foreach (var avatarItem in _avatarManager.avatars)
        {
            
            playerID = avatarItem.Key + 1;
            model.scoreBoardText += _avatarManager.avatars[avatarItem.Key].gameObject.GetComponent<color_Player>().GetName() + ": " + _avatarManager.avatars[avatarItem.Key].gameObject.GetComponentInChildren<playerScoreScript>().GetScore() + "\n";
        }
    }
    public void setColors()
    {
        foreach (var avatarItem in _avatarManager.avatars)
        {
            _avatarManager.avatars[avatarItem.Key].gameObject.GetComponent<color_Player>().updateBall();
        }
    }
    //part of score change when goal is hit
    public void SetScoreForPlayer(int clientID, int points)
    {
        _avatarManager.avatars[clientID].gameObject.GetComponentInChildren<playerScoreScript>().SetScore(points);
        setScoreBoardText();
    }
}

