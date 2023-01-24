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

    private bool returning = false;
    private string[] garbageTags;

    private TrailRenderer harpoonTrail;
    private GameObject harpoonOnHitEffect;

    private void Start()
    {
        harpoonTrail = GetComponent<TrailRenderer>();
        harpoonOnHitEffect = GameObject.Find("HarpoonOnHitEffect");
        rb = GetComponent<Rigidbody2D>();
        fixedJoint = GetComponent<FixedJoint2D>();
        garbageTags = garbageSpawner.GetGarbageTags();
    }

    private void Update()
    {

        if (returning)
        {
            Vector2 direction = (startPos.position - transform.position).normalized;
            rb.velocity = returnSpeed * direction;           
        }

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

        bool outOfBounds = !Screen.safeArea.Contains(pos);
        if (outOfBounds) returning = true;


        //if harpoon in the ship, we desabling trail, otherwise enabling
        if (startPos.position == gameObject.transform.position)
        {
            harpoonTrail.enabled = false;
        } else
        {
            harpoonTrail.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Array.Exists(garbageTags, element => element == collision.gameObject.tag) && item == null)
        {
            harpoonOnHitEffect.GetComponent<ParticleSystem>().Play();
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
