using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuoteWriter : MonoBehaviour
{
    [Serializable]
    public class Quote
    {
        public string quote;
        public string author;
    }
    public string fileName;

    
    public Quote quote;
    
    public void WriteQuote()
    {
        StreamWriter writer = new StreamWriter(Application.dataPath + $"\\{fileName}", true);
        writer.WriteLine(JsonUtility.ToJson(quote));
        Debug.Log(quote);
        Debug.Log(JsonUtility.ToJson(quote));
        writer.Close();
    }
}
