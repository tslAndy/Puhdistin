using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction, bool vertical=false)
    {
        if (vertical)
            rb.velocity = direction * speed;
        else
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }
}
