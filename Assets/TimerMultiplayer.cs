using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEngine.UI;

public class TimerMultiplayer : RealtimeComponent<DoubleModel>
{
    private Text _timeText;
    public Realtime _realtime;

    // the starting time limit in milliseconds (eg. 90000 = 90 seconds)
    public double _timeLimit;
    // how much time is left
    private double _timeLeft;
    // Start is called before the first frame update
    void Awake()
    {
        // reference the text object
        _timeText = GetComponent<Text>();

        // subscribe to the DidConnectToRoom event
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    protected override void OnRealtimeModelReplaced(TimeModel previousModel, TimeModel currentModel)
    {
        if (currentModel != null)
        {
            // if the stored model is new, set the value of _startTime to the starting time limit
            // divide by 1000 to convert millisceconds to seconds
            if (currentModel.isFreshModel)
            {
                currentModel.startTime = _timeLimit / 1000;
                _timeLeft = _timeLimit / 1000;
            }
        }
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        if (model.startTime == _timeLimit / 1000)
        {
            model.startTime = _realtime.room.time + 90;
        }
        StartCoroutine("CountDown");
    }
    private IEnumerator CountDown()
    {
        while (_timeLeft > 0)
        {

            _timeLeft = CompareDates(UnixTimeStampToDateTime(model.startTime), UnixTimeStampToDateTime(_realtime.room.time));
            // converts the time left into minutes 
            _timeText.text = ((int)(_timeLeft / 60)).ToString() + ":";

            // converts the remaining time (after minutes) to seconds
            int seconds = (int)(_timeLeft % 60);
            // if seconds are less than 10, add a 0 at the beginning
            if (seconds < 10)
            {
                _timeText.text += "0" + ((int)(_timeLeft % 60)).ToString();
            }
            else
            {
                _timeText.text += ((int)(_timeLeft % 60)).ToString();
            }
            yield return null;
        }
    }

    // used to get the difference between the current time and the stored startTime
    private double CompareDates(System.DateTime startTime, System.DateTime newTime)
    {
        double elapsedTime = (startTime - newTime).TotalSeconds;

        return elapsedTime;
    }
    // The value of Realtime.Room.Time is a Epoch Unix Timestamp (see https://en.wikipedia.org/wiki/Unix_time) for details
    // This method will convert the timestamp into a date format
    public static System.DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        System.DateTime dtDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
        return dtDateTime;
    }
}
