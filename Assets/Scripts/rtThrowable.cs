using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Valve.VR.InteractionSystem;

public class rtThrowable : Throwable
{
    private RealtimeTransform rtTransform;
    public int ownership = -1;

    // Start is called before the first frame update
    void Start()
    {
        rtTransform = GetComponent<RealtimeTransform>();
        
    }

    public void Grabbed()
    {
        rtTransform.RequestOwnership();
        ownership = rtTransform.ownerIDSelf;
    }

    protected override void HandHoverUpdate( Hand hand )
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();

        if (startingGrabType != GrabTypes.None && rtTransform.ownerIDSelf == -1)
        {
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags, attachmentOffset);
            hand.HideGrabHint();
        }
    }

}

