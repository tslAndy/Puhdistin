using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField]
    TMP_Text timerText;

    [SerializeField]
    GameObject winCanvas;

    [SerializeField]
    private int startTime = 60;
    private int counter;
    private float milisecTimer = 1;
    void Start()
    {
        counter = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        milisecTimer -= Time.deltaTime;
        if(milisecTimer <= 0)
        {
            milisecTimer = 1;
            counter -= 1;
        }

        timerText.text = counter.ToString();
        if (counter <= 0)
        {
            Time.timeScale = 0;
            winCanvas.SetActive(true);
           
            this.enabled = false;
        }
    }
}
