using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class FactsTelling : MonoBehaviour
{
    [Serializable]
    public class GarbageInfo
    {
        public string tag;

        [SerializeField]
        private string[] facts;

        public string GetRandomFact() => facts[(int)Random.Range(0, facts.Length)];
    }

    [SerializeField]
    private GarbageInfo[] facts;

    public void TellFact(string tag)
    {
        GarbageInfo garbageInfo = Array.Find(facts, element => element.tag == tag);
        if (garbageInfo == null) return;

        Debug.Log($"{facts[(int)Random.Range(0, facts.Length)]}");
    }
}
