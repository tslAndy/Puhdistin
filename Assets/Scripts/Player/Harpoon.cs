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
    private OnGarbageCollecting onGarbageCollecting;

    [SerializeField]
    private GarbageSpawner garbageSpawner;

    private GameObject item;
    private Rigidbody2D rb;
    private FixedJoint2D fixedJoint;
    private Collider2D collider;

    private bool returning = false;
    private string[] garbageTags;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fixedJoint = GetComponent<FixedJoint2D>();
        garbageTags = garbageSpawner.GetGarbageTags();
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (returning)
        {
            collider.enabled = false;
            Vector2 direction = (startPos.position - transform.position).normalized;
            rb.velocity = returnSpeed * direction;
        }

        if (Vector3.Distance(startPos.position, transform.position) < 0.5)
        {
            collider.enabled = true ;
        }

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

        bool outOfBounds = !Screen.safeArea.Contains(pos);
        if (outOfBounds) returning = true;
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Array.Exists(garbageTags, element => element == collision.gameObject.tag) && item == null)
        {
            item = collision.gameObject;
            returning = true;
            fixedJoint.enabled = true;
            fixedJoint.connectedBody = item.GetComponent<Rigidbody2D>();
            onGarbageCollecting.HandleCollect(item.tag);
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
