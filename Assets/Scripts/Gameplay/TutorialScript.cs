using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private enum tutorialStages 
    {
        TutorialBegining,
        TutorialMovingPart,
        TutorialGarbageCollectingPart,
        TutorialLogBouncingPart,
        TutorialOldLogDestroyingPart,
        TutorialVacoomingPart
    }

    [SerializeField]
    private int startFrom;

    [SerializeField]
    private TextMeshProUGUI textTutorial;

    [SerializeField]
    [TextArea(3, 10)]
    private string[] tutorialTexts;

    private int counter = 0;

    private int coroutineCounter = 1;
    void Awake()
    {
        counter = startFrom;

        StartCoroutine(Print(textTutorial, tutorialTexts[counter], 0.05f));
        counter++;
        //play animation
    }

    private void Start()
    {
        OnGarbageCollecting onGarbageCollecting = GameObject.Find("Level").GetComponent<OnGarbageCollecting>();
        onGarbageCollecting.OnGarbageColldected +=  MoveNextDialog; 
    }

    private void Update()
    {
        //if clicked then timeScale = 1, coroutine timescale = 0 afte few seconds
        if ((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")))
        {
            if (tutorialTexts.Length > counter)
            {
                coroutineCounter++;
                StartCoroutine(Print(textTutorial, tutorialTexts[counter], 0.05f));               
            }
        }
    }

    IEnumerator Print(TMP_Text textBoxToPrint, string textToPrint, float delayBetweenLetters)
    {
        Debug.Log(coroutineCounter);
        if(coroutineCounter > 1)
        {
            coroutineCounter--;
            yield break;
        }
        textBoxToPrint.text = "";
        for (int i = 0; i < textToPrint.Length; i++)
        {
                textBoxToPrint.text += textToPrint[i];
            yield return new WaitForSecondsRealtime(delayBetweenLetters);
        }
        counter++;
        coroutineCounter--;
    }

    private void MoveNextDialog(int garbageValue)
    {
        counter++;
    }

}
