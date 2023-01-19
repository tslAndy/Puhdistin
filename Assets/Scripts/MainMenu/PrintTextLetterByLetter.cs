using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrintTextLetterByLetter : MonoBehaviour
{
    private TMP_Text textBoxToPrint;
    private string textToPrint;
    private float delayBetweenLetters;
    public void PrintText(TMP_Text textBox, string toPrint, float delay)
    {
        textBoxToPrint = textBox;
        textToPrint = toPrint;
        delayBetweenLetters = delay;
        StartCoroutine(Print());
    }

    IEnumerator Print()
    {
        for (int i = 0; i < textToPrint.Length; i++)
        {
            textBoxToPrint.text += textToPrint[i];
            yield return new WaitForSeconds(delayBetweenLetters);
        }
    }
}
