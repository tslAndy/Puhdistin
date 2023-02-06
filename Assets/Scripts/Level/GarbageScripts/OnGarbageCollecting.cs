using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class OnGarbageCollecting : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText, factText;

    [NonSerialized]
    public int score = 0;
    private FactsSystem factsSystem;


    private void Start()
    {
        factsSystem = GetComponent<FactsSystem>();
    }

    //Printing facts, adding poits, and deleiting garbage from moving list
    public void HandleCollect(string tag, GameObject garbage)
    {
        score++;
        scoreText.SetText($"Score: {score}");
        string fact = garbage.GetComponent<Garbage>().fact;
        Debug.Log(fact);
        factText.SetText(fact);
        ObstaclesMoverScript.RemoveObstacle(garbage);
    }
}
