using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Harpoon : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private float returnSpeed;

    [SerializeField]
    private Transform startPos;

    [SerializeField]
    private string[] garbageTags;

    private GameObject item;
    private Rigidbody2D rb;
    private FixedJoint2D fixedJoint;

    private bool returning = false;
    private int score = 0;

    private void Update()
    {
        if (returning)
        {
            Vector2 direction = (startPos.position - transform.position).normalized;
            rb.velocity = returnSpeed * direction;
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fixedJoint = GetComponent<FixedJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Array.Exists(garbageTags, element => element == collision.gameObject.tag))
        {
            item = collision.gameObject;
            returning = true;
            fixedJoint.enabled = true;
            fixedJoint.connectedBody = item.GetComponent<Rigidbody2D>();
        }
        else if (collision.gameObject.name == "Seabed")
        {
            returning = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "HarpoonStart" && returning)
        {
            if (item != null)
            {
                score++;
                scoreText.SetText($"Score: {score}");
                Destroy(item);
                fixedJoint.connectedBody = null;
                fixedJoint.enabled = false;
            }
            transform.position = startPos.position;
            rb.bodyType = RigidbodyType2D.Static;
            returning = false;
        }
    }
}
