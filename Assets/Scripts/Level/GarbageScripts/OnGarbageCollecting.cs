using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnGarbageCollecting : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText, factText;

    private int score = 0;
    private FactsSystem factsSystem;

    private void Start()
    {
        factsSystem = GetComponent<FactsSystem>();
    }

    public void HandleCollect(string tag)
    {
        score++;
        scoreText.SetText($"Score: {score}");
        string fact = factsSystem.GetRandomFact(tag);
        Debug.Log(fact);
        factText.SetText(fact);
    }
}
