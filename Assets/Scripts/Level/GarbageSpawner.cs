using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageSpawner : MonoBehaviour
{
    [SerializeField]
    private Collider2D spawnZone;

    [SerializeField]
    private int amount;

    [SerializeField]
    private GameObject[] garbages;

    private void Start()
    {
        float startX = spawnZone.bounds.min.x;
        float startY = spawnZone.bounds.min.y;

        float width = spawnZone.bounds.max.x - startX;
        float height = spawnZone.bounds.max.y - startY;

        float ratio = width / height;
        float ySteps = Mathf.Sqrt(amount / ratio);
        float xSteps = ratio * ySteps;

        float xStep = width / xSteps;
        float yStep = height / ySteps;

        for (float x = startX; x < startX + width; x += xStep)
        {
            for (float y = startY; y < startY + height; y += yStep)
            {
                int randIndex = (int) Random.Range(0, garbages.Length);
                GameObject spawned = Instantiate(garbages[randIndex], transform);

                float xRadius = spawned.GetComponent<Collider2D>().bounds.size.x / 2;
                float minX = x + xRadius;
                float maxX = x + xStep - xRadius;
                if (maxX > startX + width) maxX = startX + width - xRadius;

                float yRadius = spawned.GetComponent<Collider2D>().bounds.size.y / 2;
                float minY = y + yRadius;
                float maxY = y + yStep - yRadius;
                if (maxY > startY + height) maxY = startY + height - yRadius;

                spawned.transform.position = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), spawned.transform.position.z);
            }
        }
    }
}
