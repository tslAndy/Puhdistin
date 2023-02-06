using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private enum TutorialStages
    {
        //Few phrases with octo master
        //level 1
        TutorialBegining,

        //Now we unlocing movement for players ship
        //level 1
        TutorialMovingPart,

        //Two new mechanics, throwing harpoon and returning it
        //level 1
        TutorialGarbageCollectingPart,

        //Harpoon bouncing from log to log
        //level 2
        TutorialLogBouncingPart,

        //Destroing log mechanic
        //level 3
        TutorialOldLogDestroyingPart,

        //Vacooming smallGarbage
        //level 3
        TutorialVacoomingPart
    }

    [SerializeField]
    private TutorialStages startFrom;

    private TutorialStages currenStage;

    [SerializeField]
    private TextMeshProUGUI textTutorial;

    [SerializeField]
    private TutorialParts parts;

    private string[] textToPrint;

    private ArtificialPauseScript pauseController;

    private Animator squidAnimator;
    private Animator canvasAnimator;

    private bool isTutorialGarbageCollected = false;

    private int counter = 0;

    private int coroutineCounter = 1;

    [Serializable]
    class TutorialParts
    {
        public TutorialPart[] parts;

        public string[] this[TutorialStages stage] => GetTextByStage(stage);

        private string[] GetTextByStage(TutorialStages stage)
        {
            foreach (TutorialPart part in parts)
            {
                if (part.stage == stage)
                {
                    Debug.Log(part.stage);
                    return part.stageText;
                }
            }
            return null;
        }
    }
    [Serializable]
   class TutorialPart
    {
        public TutorialStages stage;
        [TextArea(3, 10)]
        public string[] stageText;

    }
    void Awake()
    {
        currenStage = startFrom;
        pauseController = GetComponent<ArtificialPauseScript>();
        canvasAnimator = GameObject.Find("TutorialCanvas").GetComponent<Animator>();
        squidAnimator = GameObject.Find("Squid").GetComponent<Animator>();

        pauseController.DeactivateAllMovment();
        pauseController.DeactivateAllSpawners();

        textToPrint = parts[currenStage];

        StartCoroutine(Print(textTutorial, textToPrint[counter], 0.05f));


        //play animation
    }

    private void Start()
    {
        OnGarbageCollecting onGarbageCollecting = GameObject.Find("Level").GetComponent<OnGarbageCollecting>();
        onGarbageCollecting.OnGarbageColldected += CollectTutorialGarbage;

    }

    private void Update()
    {
        switch (currenStage)
        {
            case TutorialStages.TutorialBegining:
                BegginingPartLogic();
                break;
            case TutorialStages.TutorialMovingPart:
                MovingPartLogic();
                break;
            case TutorialStages.TutorialGarbageCollectingPart:
                GarbageCollectingPartLogic();
                break;
        }
    }

    private void GarbageCollectingPartLogic()
    {
        pauseController.ActivateThroving();
        if ((counter == 0) || isTutorialGarbageCollected)
        {
            if (textToPrint.Length > counter)
            {
                coroutineCounter++;
                StartCoroutine(Print(textTutorial, textToPrint[counter], 0.05f));
            } else
            {
                canvasAnimator.SetTrigger("IsTutorialEnded");
            }
        }
    }

    private void MovingPartLogic()
    {
        squidAnimator.SetTrigger("IsPleasentEmote");
        pauseController.ActivateShipMovement();
        pauseController.ActivateBackground();
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || (Input.GetAxis("Horizontal") != 0) || (counter == 0))
        {
            Debug.Log(textToPrint.Length);
            Debug.Log(counter);
            if (textToPrint.Length > counter)
            {
                coroutineCounter++;
                StartCoroutine(Print(textTutorial, textToPrint[counter], 0.05f));
            }
            else
            {
                MoveToStage(TutorialStages.TutorialGarbageCollectingPart);
            }
        }
    }

    private void BegginingPartLogic()
    {
        squidAnimator.SetTrigger("IsIdleEmote");
        if ((Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")))
        {
            if (textToPrint.Length > counter)
            {
                coroutineCounter++;
                StartCoroutine(Print(textTutorial, textToPrint[counter], 0.05f));
            } else
            {
                MoveToStage(TutorialStages.TutorialMovingPart);
            }
        }
    }
    private void MoveToStage(TutorialStages stage)
    {
        counter = 0;
        currenStage = stage;
        textToPrint = parts[currenStage];
    }

    IEnumerator Print(TMP_Text textBoxToPrint, string textToPrint, float delayBetweenLetters)
    {
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

    private void CollectTutorialGarbage(int garbageValue)
    {
        isTutorialGarbageCollected = true;
    }
}
