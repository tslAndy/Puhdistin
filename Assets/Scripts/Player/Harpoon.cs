using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Harpoon : MonoBehaviour
{
    [SerializeField]
    private float returnSpeed;

    [SerializeField]
    private Transform startPos;

    [SerializeField]
    private GarbageSystem garbageSystem;

    private GameObject item;
    private Rigidbody2D rb;
    private FixedJoint2D fixedJoint;

    private bool returning = false;
    private string[] garbageTags;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fixedJoint = GetComponent<FixedJoint2D>();
        garbageTags = garbageSystem.garbageSpawner.GetGarbageTags();
    }

    private void Update()
    {
        if (returning)
        {
            Vector2 direction = (startPos.position - transform.position).normalized;
            rb.velocity = returnSpeed * direction;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Array.Exists(garbageTags, element => element == collision.gameObject.tag) && item == null)
        {
            item = collision.gameObject;
            returning = true;
            fixedJoint.enabled = true;
            fixedJoint.connectedBody = item.GetComponent<Rigidbody2D>();
            garbageSystem.onGarbageCollecting.HandleCollect(item.tag);
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
