using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class QuoteReader : MonoBehaviour
{
    public float delayBetweenQuotes;
    public TMP_Text[] textBars;

    private int quoteNumber = 0;
    StreamReader reader;
    void Start()
    {
        InvokeRepeating("ShowQuote", 5f, delayBetweenQuotes);
        reader = new StreamReader(Application.dataPath + "/Quotes/MainMenuQuotes.txt");
    }

    public void ShowQuote()
    {
        Quote quote = JsonUtility.FromJson<Quote>(reader.ReadLine());
        if(quote == null)
        {
            reader.Close();
            reader = new StreamReader(Application.dataPath + "/Quotes/MainMenuQuotes.txt");

            quote = JsonUtility.FromJson<Quote>(reader.ReadLine());
        }
        string textToPrint = $"{quote.quote} \n -{quote.author}";
        float delayBetweenLetters = 0.05f;

        if (quoteNumber % 2 == 0)
        {
            textBars[0].text = "";
            StartCoroutine(Print(textBars[0], textToPrint, delayBetweenLetters));
        } else
        {
            textBars[1].text = "";
            StartCoroutine(Print(textBars[1], textToPrint, delayBetweenLetters));
        }
        quoteNumber++;
    }

    IEnumerator Print(TMP_Text textBoxToPrint, string textToPrint, float delayBetweenLetters)
    {
        for (int i = 0; i < textToPrint.Length; i++)
        {
            textBoxToPrint.text += textToPrint[i];
            yield return new WaitForSeconds(delayBetweenLetters);
        }
    }
}
