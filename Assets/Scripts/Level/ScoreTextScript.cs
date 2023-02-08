using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTextScript : MonoBehaviour
{
    private TextMeshProUGUI winScoreText, score;

    private int scoreValue;

    void Start()
    {
        score = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        winScoreText = GetComponent<TextMeshProUGUI>();
        scoreValue = CleanString();
        StartCoroutine(PrintScore());
    }

    

    private int CleanString()
    {
        return Convert.ToInt32(score.text.Trim('S', 'c', 'o', 'r', 'e', ':', ' '));
    }

    IEnumerator PrintScore()
    {
        for (int i = 0; i <= scoreValue; i++)
        {
            yield return new WaitForSecondsRealtime(.005f);
            winScoreText.text = i.ToString();
        }
    }
}
