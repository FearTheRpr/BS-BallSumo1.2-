using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEngine.UI;

public class gameTimer : RealtimeComponent<TimerModel>
{
    private Text _timeText;
    public Realtime _realtime;
    public GameObject _Ring1;
    public GameObject _Ring2;
   

    // the starting time limit in milliseconds (eg. 90000 = 90 seconds)
    public double _timeLimit;
    // how much time is left
    private double timeLeft;
    // Start is called before the first frame update
    void Awake()
    {
        // reference the text object
        _timeText = GetComponent<Text>();

        // subscribe to the DidConnectToRoom event
        _realtime.didConnectToRoom += DidConnectToRoom;
    }



    protected override void OnRealtimeModelReplaced(TimerModel previousModel, TimerModel currentModel)
    {
        if (currentModel != null)
        {
            // if the stored model is new, set the value of _startTime to the starting time limit
            // divide by 1000 to convert millisceconds to seconds
            if (currentModel.isFreshModel)
            {
                currentModel.timerCode = _timeLimit / 1000;
                timeLeft = _timeLimit / 1000;
            }
        }
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        if (model.timerCode == _timeLimit / 1000)
        {
            //change if different end time is desired (600 = 10 min)
            model.timerCode = _realtime.room.time + 312;
        }

       

        StartCoroutine("CountDown");
    }
    private IEnumerator CountDown()
    {
        while (timeLeft > 0)
        {

            timeLeft = CompareDates(UnixTimeStampToDateTime(model.timerCode), UnixTimeStampToDateTime(_realtime.room.time));
            // converts the time left into minutes 
            _timeText.text = ((int)(timeLeft / 60)).ToString() + ":";

            // converts the remaining time (after minutes) to seconds
            int seconds = (int)(timeLeft % 60);
            // if seconds are less than 10, add a 0 at the beginning
            if (seconds < 10)
            {
                _timeText.text += "0" + ((int)(timeLeft % 60)).ToString();
            }
            else
            {
                _timeText.text += ((int)(timeLeft % 60)).ToString();
            }
            yield return null;



            //lowers the outter ring when the timer says 5 min remaining
            if (_timeText.text == "5:00" && _Ring1 != null)
            {

                Debug.Log("Time Left 5 min");
                _Ring1.transform.position += new Vector3(0, -10, 0) * Time.deltaTime;
                Destroy(_Ring1, 5);
               
            }
            //lowers the outter ring when the timer says 2 asnd a half min remaining
            if (timeLeft < 150 && _Ring2 != null)
            {
                Debug.Log("Time Left 2.5 min");
                _Ring2.transform.position += new Vector3(0, -10, 0) * Time.deltaTime;
                Destroy(_Ring2, 5);
            }
            //what happens when timer runs out
            if (timeLeft == 0.01)
            {
                Debug.Log("Game Over");

            }

            else
            {
                        Debug.Log("waiting");
            }


        }

    }

    // used to get the difference between the current time and the stored startTime
    private double CompareDates(System.DateTime timerCode, System.DateTime newTime)
    {
        double elapsedTime = (timerCode - newTime).TotalSeconds;

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
