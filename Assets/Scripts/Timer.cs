using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 60f;
    public bool timeIsRunning = false;
    public TMP_Text timeText;

    public GameObject gameOverPanel;
    void Start()
    {
        //This adds to the time statement to a true or false if time is running every seconds in the game
        timeIsRunning = true;


        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining > 0)
            {
                //This tells the timer that you want negative instead of postive, so you want to continue to count down instead of counting up
                timeRemaining -= Time.unscaledDeltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                //If the timer stops at 0 for its position, it will call on the game to have a UI game over screen
                timeRemaining = 0;
                timeIsRunning = false;
                GameOver();
            }
        }
    }

    void DisplayTime (float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        //This goes upon the timer if the mintues and seconds are both called and will work accordinaly to its interger position
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }


    void GameOver()
    {
        //If game over is called than this has to be set to true to make that function happen
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
