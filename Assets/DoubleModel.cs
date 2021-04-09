using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;


[RealtimeModel]
public partial class DoubleModel
{
    [RealtimeProperty(1, false, true)]
    private double _countdown;

}

