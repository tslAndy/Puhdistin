using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class GarbageSpawner : ItemSpawner
{

    public override IEnumerator DelayedDisabling(GameObject spawned)
    {
        yield return new WaitForSeconds(1f);
        Destroy(spawned.GetComponent<Rigidbody2D>());
        Destroy(spawned.GetComponent<CapsuleCollider2D>());
    }
}
