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

    private GarbageSystem garbageSystem;

    private bool spawned;

    void Start()
    {
        garbageSystem = GetComponent<GarbageSystem>();
    }


    void Update()
    {
        if (spawned) return;

        if (garbageSystem.garbageSpawner.spawnedAmount == garbageSystem.onGarbageCollecting.score)
            SpawnFishes();
    }

    private void SpawnFishes()
    {
        Collider2D spawnZone = garbageSystem.garbageSpawner.spawnZone;
        float yStep = (spawnZone.bounds.max.y - spawnZone.bounds.max.y) / fishesAmount;

        Random rand = new Random();

        for (int i = 0; i < fishesAmount; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[rand.Next(0, fishPrefabs.Length)], transform);
            fish.transform.position = new Vector3(0, i * yStep * 1.5f, transform.position.z);

        }
    }
}
