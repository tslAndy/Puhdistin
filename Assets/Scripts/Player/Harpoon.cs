using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public Transform harpoonSpawner;
    private Rigidbody2D rb;
    private FixedJoint2D fixedJoint;
    private GameObject item;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fixedJoint = GetComponent<FixedJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Garbage"))
        {
            if (item != null) return;
            item = collision.gameObject;
            fixedJoint.enabled = true;
            fixedJoint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 direction = (harpoonSpawner.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("huhuh");
        if (collision.gameObject.name == "HarpoonSpawner" && item != null)
        {
            Destroy(item);
            Destroy(gameObject);
        }
    }
}
