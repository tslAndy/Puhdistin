using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class FishSpawning : MonoBehaviour
{
    [SerializeField]
    private int fishesAmount;

    [SerializeField]
    private GameObject[] fishPrefabs;

    private OnGarbageCollecting onGarbageCollecting;
    private GarbageSpawner garbageSpawner;

    private bool spawned;

    void Start()
    {
        onGarbageCollecting = GetComponent<OnGarbageCollecting>();
        garbageSpawner = GetComponent<GarbageSpawner>();
    }


    void Update()
    {
        if (spawned) return;

        if (garbageSpawner.spawnedAmount == onGarbageCollecting.score)
            SpawnFishes();
    }

    private void SpawnFishes()
    {
        Collider2D spawnZone = garbageSpawner.spawnZone;
        float yStep = (spawnZone.bounds.max.y - spawnZone.bounds.min.y) / fishesAmount;

        Random rand = new Random();

        for (int i = 0; i < fishesAmount; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[rand.Next(0, fishPrefabs.Length)], transform);
            
            float dx = (spawnZone.bounds.max.x - spawnZone.bounds.min.x) / 2 + 1;
            if (rand.Next(0, 4) % 2 == 0)
                dx *= -1;

            float x = spawnZone.bounds.center.x + dx;
            float y = spawnZone.bounds.min.y + i * yStep + yStep / 2;
            fish.transform.position = new Vector3(x, y, transform.position.z);
        }

        spawned = true;
    }
}
