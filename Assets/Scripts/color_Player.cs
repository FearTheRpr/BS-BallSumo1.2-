using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;



public class color_Player : RealtimeComponent<color_Model>
{

    //renderer to set the color
    public Renderer Ballcolor;
    public string TestName;
    public playerScoreScript PSS;
    protected override void OnRealtimeModelReplaced(color_Model previousModel, color_Model currentModel)
    {
        if (previousModel != null)
        {
            previousModel.pColorDidChange -= PColorDidChange;
            previousModel.pNameDidChange -= PNameDidChange;
        }
        if (currentModel != null)
        {
            if (currentModel.isFreshModel)
            {
                
                model.pColor = new Color(0f,0f,0f);
                model.pName = string.Empty;
            }
            updateBall();
            // subscrib to the events of change
            currentModel.pColorDidChange += PColorDidChange;
            currentModel.pNameDidChange += PNameDidChange;
        }

    }
    private void Start()
    {
        if (this.isOwnedLocallyInHierarchy)
        {
            getLocalVals();
        }
    }
    public void PNameDidChange(color_Model Model, string nam)
    {
        PSS.updateName();
    }
    //set the color of avatar ball
    public void PColorDidChange(color_Model Model, Color Cul)
    {
        updateBall();
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
    public void updateBall()
    {
        Ballcolor.material.color = model.pColor;
    }
    private void getLocalVals()
    {
     FindObjectOfType<Color_Name>().putToRT();
    }
}
