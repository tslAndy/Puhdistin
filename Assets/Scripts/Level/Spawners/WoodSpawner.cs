using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSpawner : ItemSpawner
{   
    public override void SpawnItems()
    {
        base.SpawnItems();
        spawned.transform.rotation = new Quaternion(spawned.transform.rotation.x, spawned.transform.rotation.y, Random.Range(0, 181), spawned.transform.rotation.w);
    }
}
