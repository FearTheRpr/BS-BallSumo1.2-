using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Valve.VR.InteractionSystem;

public class rtTimer : MonoBehaviour
{ 
    private RealtimeView rtView;
    private RealtimeTransform rtTransfrom;

    // Start is called before the first frame update
    void Start()
    {
        rtView = GetComponent<RealtimeView>();
        rtTransfrom = GetComponent<RealtimeTransform>();
    }

    // Update is called once per frame
    public void timerStarted()
    {
        rtView.RequestOwnership();
        rtTransfrom.RequestOwnership();

    }
}
