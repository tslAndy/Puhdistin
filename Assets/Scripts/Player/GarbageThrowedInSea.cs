using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GarbageThrowedInSea : MonoBehaviour
{
    [SerializeField]
    private  TextMeshProUGUI  score;

    [SerializeField]
    private GameObject thankYouCanvas;

    public static int scoreValue = 0;


    private void OnLevelWasLoaded(int level)
    {
        Time.timeScale = 1;
    }
    void Start()
    {
        scoreValue = TotalScoreScript.totalPlayerScore * 74 + 23;

        StartCoroutine(PrintScore());
    }


    IEnumerator PrintScore()
    {
        for (int i = (int)(scoreValue * 0.99f); i <= scoreValue; i++)
        {
            yield return new WaitForSecondsRealtime(0.000005f);

                Debug.Log(scoreValue);
                Debug.Log("Total score: " + i);
                score.text = "Garbage throwed in sea, when you played:  " + i;

        }
        StartCoroutine(TransferToTheNextScene());

    }   
    IEnumerator TransferToTheNextScene(){
        yield return new WaitForSecondsRealtime(5f);
        thankYouCanvas.SetActive(true);
        GameObject.Find("Stats").SetActive(false);
    }
}


