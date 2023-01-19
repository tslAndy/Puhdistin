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
        InvokeRepeating("ShowQuote", 0.5f, delayBetweenQuotes);
        reader = new StreamReader(Application.dataPath + "/MainMenuQuotes");
    }

    public void ShowQuote()
    {
        Quote quote = JsonUtility.FromJson<Quote>(reader.ReadLine());
        Debug.Log(quote.quote);
        if (quoteNumber % 2 == 0)
        {
            textBars[0].text = $"{quote.quote} \n -{quote.author}";
        } else
        {
            textBars[1].text = $"{quote.quote} \n -{quote.author}";
        }
        quoteNumber++;
    }
}
