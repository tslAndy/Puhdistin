using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class GarbageSpawner : MonoBehaviour
{
    public Collider2D spawnZone;

    [SerializeField]
    private int amount;

    [SerializeField]
    private GameObject[] garbagePrefabs;

    [NonSerialized]
    public int spawnedAmount;

    private float startX;
    private float startY;

    private float width;
    private float height;

    private float ratio;
    private float ySteps;
    private float xSteps;

    private float xStep;
    private float yStep;


    private void Start()
    {
        startX = spawnZone.bounds.min.x;
        startY = spawnZone.bounds.min.y;

        width = spawnZone.bounds.max.x - startX;
        height = spawnZone.bounds.max.y - startY;

        ratio = width / height;
        ySteps = Mathf.Sqrt(amount / ratio);
        xSteps = ratio * ySteps;

        xStep = width / xSteps;
        yStep = height / ySteps;

        InvokeRepeating("SpawnGarbage", 0f, 4f);
    }

    IEnumerator DelayedDisabling(GameObject spawned)
    {
        yield return new WaitForSeconds(1f);
        spawned.GetComponent<CapsuleCollider2D>().enabled = false;
        Destroy(spawned.GetComponent<Rigidbody2D>());
    }
    public string[] GetGarbageTags()
    {
        string[] garbageTags = new string[garbagePrefabs.Length];

        for (int i = 0; i < garbagePrefabs.Length; i++)
            garbageTags[i] = garbagePrefabs[i].tag;
        
        return garbageTags;
    }

    private void SpawnGarbage()
    {
        for (float x = startX; x < startX + width; x += xStep)
        {
            for (float y = startY; y < startY + height; y += yStep)
            {
                int randIndex = (int)Random.Range(0, garbagePrefabs.Length);
                GameObject spawned = Instantiate(garbagePrefabs[randIndex], transform);
                spawned.transform.parent = null;

                float xRadius = spawned.GetComponent<Collider2D>().bounds.size.x / 2;
                float minX = x + xRadius;
                float maxX = x + xStep - xRadius;
                if (maxX > startX + width) maxX = startX + width - xRadius;

                float yRadius = spawned.GetComponent<Collider2D>().bounds.size.y / 2;
                float minY = y + yRadius;
                float maxY = y + yStep - yRadius;
                if (maxY > startY + height) maxY = startY + height - yRadius;

                Vector2 position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));




                spawned.transform.position = position;
                ObstaclesMoverScript.AddObstacle(spawned);
                StartCoroutine(DelayedDisabling(spawned));

                spawnedAmount++;
            }
        }
    }
}
