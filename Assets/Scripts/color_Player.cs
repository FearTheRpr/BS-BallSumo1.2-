using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;



public class color_Player : RealtimeComponent<color_Model>
{
    public Renderer Ballcolor;
    protected override void OnRealtimeModelReplaced(color_Model previousModel, color_Model currentModel)
    {
        if (previousModel != null)
        {
            previousModel.pScoreDidChange -= PScoreDidChange;
            previousModel.pColorDidChange -= PColorDidChange;
            previousModel.pNameDidChange -= PNameDidChange;
        }
        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                model.pScore = 0;
            }
            //update the core to zero to make sure all the code nows
            UpdateScore();
            // subscrib to the event of playerscore change
            currentModel.pScoreDidChange += PScoreDidChange;
            currentModel.pColorDidChange += PColorDidChange;
            currentModel.pNameDidChange += PNameDidChange;
        }

    }
    public void PNameDidChange(color_Model Model, string nam)
    {

    }
    public void PColorDidChange(color_Model Model, Color Cul)
    {
        Ballcolor.material.color = model.pColor;
    }
    public void PScoreDidChange(color_Model Model, int value)
    {

    }
    public void UpdateScore()
    {

    }
    public void setColor(Color V)
    {
        model.pColor = V;
    }
    public void setPlayerName(string N)
    {
        model.pName = N;
    }
}
