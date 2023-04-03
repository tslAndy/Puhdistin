using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishesSavedScore : MonoBehaviour
{
    [SerializeField]
    private  TextMeshProUGUI  score;

    public static int scoreValue = 0;


    private void OnLevelWasLoaded(int level)
    {
        Time.timeScale = 1;
    }
    void Start()
    {
        scoreValue = TotalScoreScript.totalPlayerScore * 3;

        StartCoroutine(PrintScore());
    }


    IEnumerator PrintScore()
    {
        for (int i = (int)(scoreValue * 0.8f); i <= scoreValue; i++)
        {
            yield return new WaitForSecondsRealtime(0.0005f);

                score.text = "Fishes Saved: " + i;

            }

        }
    }

