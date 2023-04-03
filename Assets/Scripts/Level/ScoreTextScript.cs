using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextScript : MonoBehaviour
{
    private static TextMeshProUGUI winScoreText, score;

    public static int scoreValue = 0;

    public bool useTotalScore = false;

    private float delay = 0.05f;

    private void OnLevelWasLoaded(int level)
    {
        scoreValue = 0;
        Time.timeScale = 1;
    }
    void Start()
    {
        score = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        winScoreText = GetComponent<TextMeshProUGUI>();
        if (useTotalScore)
        {
            scoreValue = TotalScoreScript.totalPlayerScore;
        } else
        {
            scoreValue = CleanString();
        }

        if(scoreValue > 15)
        {
            delay = 0.005f;
        }
        if(scoreValue > 100)
        {
            delay = 0.0005f;
        }
        StartCoroutine(PrintScore());
    }

    

    public static int CleanString()
    {
        return Convert.ToInt32(score.text.Trim('S', 'c', 'o', 'r', 'e', ':', ' '));
    }

    IEnumerator PrintScore()
    {
        for (int i = (int)(scoreValue * 0.8f); i <= scoreValue; i++)
        {
            yield return new WaitForSecondsRealtime(delay);
            if (useTotalScore)
            {
                Debug.Log(scoreValue);
                Debug.Log("Total score: " + i);
                score.text = "Total score: " + i;

            } else
            {
                winScoreText.text = i.ToString();
            }
        }
    }
}
