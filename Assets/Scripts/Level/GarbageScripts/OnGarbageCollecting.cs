using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class OnGarbageCollecting : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText, factText, scoreEffectText;

    public event Action<int> OnGarbageColldected;

    [NonSerialized]
    public int score = 0;
    private FactsSystem factsSystem;


    private int coroutineCounter = 1;


    private void Start()
    {
        factsSystem = GetComponent<FactsSystem>();
    }

    //Printing facts, adding poits, and deleiting garbage from moving list
    public void HandleCollect(string tag, GameObject garbage)
    {
        int addedValue = garbage.GetComponent<Garbage>().value;
        score += addedValue;
        TotalScoreScript.totalPlayerScore += addedValue;
        ScoreTextScript.scoreValue += addedValue;
        Debug.Log(TotalScoreScript.totalPlayerScore);
        OnGarbageColldected?.Invoke(garbage.GetComponent<Garbage>().value);

        scoreText.SetText($"Score: {score}");
        string fact = garbage.GetComponent<Garbage>().fact;
        StartCoroutine(Print(factText, fact, .05f));
        coroutineCounter++;
        ObstaclesMoverScript.RemoveObstacle(garbage);
    }

    IEnumerator Print(TMP_Text textBoxToPrint, string textToPrint, float delayBetweenLetters)
    {
        if (coroutineCounter > 1)
        {
            coroutineCounter--;
            yield break;
        }
        textBoxToPrint.text = "";
        for (int i = 0; i < textToPrint.Length; i++)
        {
            textBoxToPrint.text += textToPrint[i];
            yield return new WaitForSeconds(delayBetweenLetters);
        }
        coroutineCounter--;
    }

    public void DeactivateEffect()
    {
        scoreEffectText.enabled = false;
    }
}
