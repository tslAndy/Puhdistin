using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
    public Collider2D spawnZone;

    [SerializeField]
    private int amount;

    [SerializeField]
    private GameObject[] itemsPrefabs;

    [NonSerialized]
    public int spawnedAmount;

    protected GameObject spawned;

    private float startX, startY;
    private float width, height;

    private float ratio;
    private float ySteps, xSteps;

    private float xStep, yStep;

    private List<GameObject> items = new List<GameObject>();

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

        InvokeRepeating("SpawnItems", 0f, 4f);
    }

    public virtual IEnumerator DelayedDisabling(GameObject spawned)
    {
        yield return new WaitForSeconds(1f);
        Destroy(spawned.GetComponent<Rigidbody2D>());
    }

    public string[] GetItemsTags()
    {
        string[] itemsTags = new string[itemsPrefabs.Length];

        for (int i = 0; i < itemsPrefabs.Length; i++)
            itemsTags[i] = itemsPrefabs[i].tag;

        return itemsTags;
    }

    public virtual void SpawnItems()
    {
        for (float x = startX; x < startX + width; x += xStep)
        {
            for (float y = startY; y < startY + height; y += yStep)
            {
                int randIndex = (int)Random.Range(0, itemsPrefabs.Length);
                spawned = Instantiate(itemsPrefabs[randIndex], transform);
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
                items.Add(spawned);
                ObstaclesMoverScript.AddObstacle(spawned);
                StartCoroutine(DelayedDisabling(spawned));

                spawnedAmount++;
            }
        }
    }

    private void FixedUpdate() => ClearGarbage();

    private void ClearGarbage()
    {
        foreach (GameObject item in items)
        {
            if (item != null && item.transform.position.x < -14f)
            {
                ObstaclesMoverScript.RemoveObstacle(item);
                Destroy(item);
            }
        }
    }
}
