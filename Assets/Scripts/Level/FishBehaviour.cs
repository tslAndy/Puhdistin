using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class FishBehaviour : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Vector2 velocity;
    private Rigidbody2D rb;
    private Collider2D spawnZone;

    private bool canMove; 
    private void Start()
    {
        velocity = Vector2.right;
        rb = GetComponent<Rigidbody2D>();
        spawnZone = GameObject.Find("Level").GetComponent<Collider2D>();
        StartCoroutine(StartDelay());
    }

    private void Update()
    {
        if (!canMove) return;

        if (transform.position.x > spawnZone.bounds.max.x || transform.position.x < spawnZone.bounds.min.x)
        {
            Debug.Log("changing direction because of position outside of screen");
            float direction = Mathf.Sign(spawnZone.bounds.center.x - transform.position.x);
            ChangeDirection(direction);
        }
        rb.velocity = velocity * speed;
    }

    private void ChangeDirection(float direction)
    {
        if (Mathf.Sign(velocity.x) != direction)
            velocity *= -1;
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(Random.Range(1, 5));
        canMove = true;
    }
}
