using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using Cinemachine;

public class FactsSystem : MonoBehaviour
{
    public GarbageInfo[] garbageInfos;

    public string GetRandomFact(string garbageTag)
    {
        Debug.Log("Getting fact about " + garbageTag);
        GarbageInfo garbageInfo = Array.Find(garbageInfos, element => element.garbageTag == garbageTag);
        if (garbageInfo == null) return null;

        if (garbageInfo.facts.Length == 1) return garbageInfo.facts[0];

        Random rand = new Random();
        string randomFact = null;
        while (randomFact == garbageInfo.prevFact)
            randomFact =  garbageInfo.facts[rand.Next(0, garbageInfo.facts.Length)];

        garbageInfo.prevFact = randomFact;
        return randomFact;
    }

    public string[] GetGarbageTags()
    {
        string[] garbageTags = new string[garbageInfos.Length];
        for (int i = 0; i < garbageInfos.Length; i++)
            garbageTags[i] = garbageInfos[i].garbageTag;
        return garbageTags;
    }
}

[Serializable]
public class GarbageInfo
{
    [TagField]
    [SerializeField]
    public string garbageTag;

    [TextArea(3, 10)]
    public string[] facts;

    [NonSerialized]
    public string prevFact;
}