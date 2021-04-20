using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;



public class color_Player : RealtimeComponent<color_Model>
{
    //renderer to set the color
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
            // subscrib to the events of change
            currentModel.pScoreDidChange += PScoreDidChange;
            currentModel.pColorDidChange += PColorDidChange;
            currentModel.pNameDidChange += PNameDidChange;
        }

    }
    public void PNameDidChange(color_Model Model, string nam)
    {

    }
    //set the color of avatar ball
    public void PColorDidChange(color_Model Model, Color Cul)
    {
        Ballcolor.material.color = model.pColor;
    }
    public void PScoreDidChange(color_Model Model, int value)
    {

    }
    public void setVelocityofP(Vector3 V)
    {
        model.pVelocity = V;
    }
    //getter method for velocity
    public Vector3 getVelocityofP()
    {
        return model.pVelocity;
    }
    //setter method for color
    public void setColor(Color V)
    {
        model.pColor = V;
    }
    //setter method for name
    public void setPlayerName(string N)
    {
        model.pName = N;
    }

    public string GetName()
    {
        return model.pName;
    }
}
