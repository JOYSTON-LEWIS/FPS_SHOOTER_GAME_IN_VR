using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // these wont change throughout the game hence
    // make them static
    public static int score, highScore;

    // variables for referencing the text in UI
    public TextMeshProUGUI scoreText, scoreNumber;

    // 2 functions set highscore and reset score

    // function to set the highscore
    public void SetHighScore()
    {
        // logic for highscore to get updated only
        // if score is higher than highscore
        if(score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);
        }

        highScore = PlayerPrefs.GetInt("highscore");

        scoreNumber.text = "" + highScore;
        scoreText.text = "High Score";
    }

    // function to reset the score
    public void ResetScore()
    {
        score = 0;
        scoreNumber.text = "" + score;
        scoreText.text = "Score";
    }

    // function to add score for enemies destroyed
    public void AddPoints(int pointsToAdd)
    {
        // if two different enemies have different points
        // to get added when destroyed, that can be configured
        // use this function as generalised function
        score += pointsToAdd;
        scoreNumber.text = "" + score;

    }

    // function to reduce enemy health, can create some
    // negative debuffs for players points to get reduced
    /*
    public void SubPoints(int pointsToSub)
    {
        score -= pointsToSub;
        scoreNumber.text = "" + score;
    }
    */

    // We dont require the following functions
    /*
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    */
}
