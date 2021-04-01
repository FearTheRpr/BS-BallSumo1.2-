using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Getball : MonoBehaviour
{
    private GameObject ball;
    public Object ballprefab;
    private void Awake()
    {
        ball = Realtime.Instantiate("PLay_Ball");
        ball.transform.SetParent(null);
        this.transform.SetParent(ball.transform);
    }
}
