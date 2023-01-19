using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuoteWriter))]
public class CustomEditorForQuotesWriter : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        QuoteWriter myScript = (QuoteWriter)target;
        if (GUILayout.Button("Write a quote"))
        {
            myScript.WriteQuote();
        }
    }
}
