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

    [SerializeField]
    private GarbageValue[] garbageValue;

    [Serializable]
    class GarbageValue
    {
        public string tag;
        public int value;
    }

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
        for (int i = 0; i < garbageValue.Length; i++)
        {
            if (tag == garbageValue[i].tag)
            {
                score += garbageValue[i].value;
                OnGarbageColldected?.Invoke(garbageValue[i].value);
            }
        }
        scoreText.SetText($"Score: {score}");
        string fact = garbage.GetComponent<Garbage>().fact;
        Debug.Log(fact);
        StartCoroutine(Print(factText, fact, .05f));
        ObstaclesMoverScript.RemoveObstacle(garbage);
    }

    IEnumerator Print(TMP_Text textBoxToPrint, string textToPrint, float delayBetweenLetters)
    {
        textBoxToPrint.text = "";
        for (int i = 0; i < textToPrint.Length; i++)
        {
            textBoxToPrint.text += textToPrint[i];
            yield return new WaitForSeconds(delayBetweenLetters);
        }
    }

    public void DeactivateEffect()
    {
        scoreEffectText.enabled = false;
    }
}
