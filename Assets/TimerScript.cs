using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Normal.Realtime;

public class TimerScript : RealtimeComponent<TimerModel>
{
    private float timeRemainingten = 600;
    public Text timeText;
    public bool countDown = false;
    private Text _text;

  void Start()
    {
        _text = timeText;
    }
    public void tenMinuteTimer()
    {
        countDown = true;
    }

    private void Update()
    {
        //detects game start
        if (countDown == true)
        {
            //if the timer isnt at 0 then it continues timing down
            if (timeRemainingten > 0)
            {
                timeRemainingten -= Time.deltaTime;
                DisplayTime(timeRemainingten);
            }

            //ends count down and game
            if (timeRemainingten < 0)
            {
                Debug.Log("Sees timer has hit 0");
                timeRemainingten = 0;
                countDown = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        //Displayes time in 12 hour format
        timeToDisplay += 1;

    float minutes = Mathf.FloorToInt(timeToDisplay / 60);
    float seconds = Mathf.FloorToInt(timeToDisplay % 60);
    timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }



}
