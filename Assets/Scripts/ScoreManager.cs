using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int score;
    public int comboCount;
    public bool lastClearWasBig;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void AddScore(int amount)
    {
        //The amount of score that you have will indicated how much is being added by every piece that clears the space
        score += amount;
        Debug.Log("Score:" + score);
    }

    public void OnLinesCleared(int lines)
    {
        int basePoints = 0;

        //This shows how much points you get when you fill the gaps on the board
        switch (lines)
        {
            case 1: basePoints = 100; break;
            case 2: basePoints = 300; break;
            case 3: basePoints = 500; break;
            case 4: basePoints = 800; break;
        }

        ScoreManager.instance.AddScore(basePoints);

        HandleCombo(lines);
        HandleExtraCombo(lines);
    }

    public void HandleCombo(int lines)
    {
        if (lines > 0)
        {
            //The function to where you clear enough lines you get a combo added to the game
            ScoreManager.instance.comboCount++;
            int comboBonus = ScoreManager.instance.comboCount * 50;
            ScoreManager.instance.AddScore(comboBonus);
        }
        else
        {
            ScoreManager.instance.comboCount = 0;
        }
    }

    public void HandleExtraCombo(int lines)
    {

        //If you clear a big line of rows on the tetris board you get a bonus score of 400 points
        bool isBigClear = (lines >= 4);

        if (isBigClear)
        {
            if (ScoreManager.instance.lastClearWasBig)
            {
                ScoreManager.instance.AddScore(400);
            }

            ScoreManager.instance.lastClearWasBig = true;
        }
        else
        {
            ScoreManager.instance.lastClearWasBig = false;
        }
    }


    //This adds to the score speed bonus if you clear a line quicker than taking a lot of turns slowly clearing the gap in lines on the board
    public void AddSpeedBonus(float timeSinceLastClear)
    {
        if (timeSinceLastClear < 1f)
        {
            ScoreManager.instance.AddScore(200);
        }
        else if (timeSinceLastClear < 2f)
        {
            ScoreManager.instance.AddScore(100);
        }
    }    
}
