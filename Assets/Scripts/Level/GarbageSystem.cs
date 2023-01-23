using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GarbageSystem : MonoBehaviour
{
    [NonSerialized]
    public GarbageSpawner garbageSpawner;

    [NonSerialized]
    public OnGarbageCollecting onGarbageCollecting;

    private void Start()
    {
        garbageSpawner = GetComponent<GarbageSpawner>();
        onGarbageCollecting = GetComponent<OnGarbageCollecting>();
    }
}