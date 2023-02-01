using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallGarbageSpawner : ItemSpawner
{
    public override IEnumerator DelayedDisabling(GameObject spawned)
    {
        yield return new WaitForSeconds(0f);
    }
}
