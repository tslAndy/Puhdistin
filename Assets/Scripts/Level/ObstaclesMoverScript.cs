using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesMoverScript : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private static List<GameObject> obstacles = new List<GameObject>();

    public static void AddObstacle(GameObject obstacle)
    {
        obstacles.Add(obstacle);
    }

    private void Update()
    {
        Debug.LogWarning(obstacles);
        Debug.LogWarning(obstacles.Count);
        foreach (var item in obstacles)
        {
            item.transform.position -= new Vector3(speed * Time.deltaTime, 0);
        }
    }
}
