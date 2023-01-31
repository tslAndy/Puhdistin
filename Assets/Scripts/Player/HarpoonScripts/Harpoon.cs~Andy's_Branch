using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Harpoon : MonoBehaviour
{
    [SerializeField]
    private string returningLayer;



    [SerializeField]
    private Transform startPos;

    [Header("GarbageScripts")]
    [SerializeField]
    private OnGarbageCollecting onGarbageCollecting;

    [SerializeField]
    private GarbageSpawner garbageSpawner;

    [Header("ReturnMovmentThings")]
    [SerializeField]
    private ContactFilter2D movmentFilter;

    [SerializeField]
    private float collisionOffset;
    
    [SerializeField]
    private float returnSpeed;

    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    private GameObject item;
    private Rigidbody2D rb;
    private FixedJoint2D fixedJoint;

    private bool returning = false;
    private string[] garbageTags;

    private TrailRenderer harpoonTrail;
    private GameObject harpoonOnHitEffect;

    private string defaultLayer;

    private void Start()
    {
        harpoonTrail = GetComponent<TrailRenderer>();
        harpoonOnHitEffect = GameObject.Find("HarpoonOnHitEffect");
        rb = GetComponent<Rigidbody2D>();
        fixedJoint = GetComponent<FixedJoint2D>();
        garbageTags = garbageSpawner.GetItemsTags();
        defaultLayer = LayerMask.LayerToName(gameObject.layer);
    }

    private void Update()
    {
        if (returning)
        {
            if (item != null)
            {
                gameObject.layer = LayerMask.NameToLayer(returningLayer);
                item.layer = LayerMask.NameToLayer(returningLayer);
            }
            Vector2 direction = (startPos.position - transform.position).normalized;

            ReturnHarpoon(direction);
        }

        if (Vector3.Distance(startPos.position, transform.position) < 0.5 && item != null)
        {
            gameObject.layer = LayerMask.NameToLayer(defaultLayer);
        }

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

        bool outOfBounds = !Screen.safeArea.Contains(pos);
        if (outOfBounds) returning = true;


        //if harpoon in the ship, we desabling trail, otherwise enabling
        if (startPos.position == gameObject.transform.position)
        {
            harpoonTrail.enabled = false;
            gameObject.transform.position = startPos.position;
        } else
        {
            harpoonTrail.enabled = true;
        }

        if (Input.GetMouseButton(1))
        {
            returning = true;
        } else if(!outOfBounds)
        {
            returning = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Array.Exists(garbageTags, element => element == collision.gameObject.tag) && item == null)
        {

            PlayHarpoonAnimation(collision);
            item = collision.gameObject;
            fixedJoint.enabled = true;
            fixedJoint.connectedBody = item.GetComponent<Rigidbody2D>();
            //onGarbageCollecting.HandleCollect(item.tag);
        }
    }

    private void ReturnHarpoon(Vector2 direction)
    {
        bool success = MoveHarpoon(direction);
        if (!success)
        {
            //Trying to move harpoon in right / left
            success = MoveHarpoon(new Vector2(direction.x, 0));
            if (!success)
            {
                success = MoveHarpoon(new Vector2(0, direction.y));
            }
        }
    }
    
    //Moving harpoon in concrete direction
    private bool MoveHarpoon(Vector2 direction)
    {       

        int count = rb.Cast(
            direction,
            movmentFilter,
            castCollisions,
            returnSpeed * Time.deltaTime + collisionOffset
        );

        if (count == 0 || count == 1)
        {
            rb.velocity = returnSpeed * direction;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PlayHarpoonAnimation(Collision2D collision)
    {
        Destroy(collision.gameObject.GetComponent<ParticleSystem>());
        harpoonOnHitEffect.GetComponent<ParticleSystem>().Play();
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
