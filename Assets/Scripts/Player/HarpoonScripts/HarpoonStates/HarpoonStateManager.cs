using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonStateManager : MonoBehaviour
{

    public float speed;

    public GameObject harpoon;

    public GameObject harpoonStartPoint;
    public string returningLayer;

    [HideInInspector]
    public Rigidbody2D harpoonRb;

    [Header("ReturnMovmentThings")]
    public ContactFilter2D movmentFilter;

    public float collisionOffset;

    public float returnSpeed;

    [HideInInspector]
    public List<RaycastHit2D> castCollisions;

    [Header("GarbageScripts")]
    public OnGarbageCollecting onGarbageCollecting;
    
    public GarbageSpawner garbageSpawner;

    [HideInInspector]
    public GameObject garbage;

    [HideInInspector]
    public string[] garbageTags;

    [HideInInspector]
    public ParticleSystem harpoonOnHitEffect;

    [HideInInspector]
    public TrailRenderer harpoonTrail;

    [HideInInspector]
    public string defaultLayer;

    private HarpoonBaseState currentState;
    public HarpoonInShipState inShipState = new HarpoonInShipState();
    public HarpoonThrowedState throwedState = new HarpoonThrowedState();
    public HarpoonReturningState returningState = new HarpoonReturningState();
    void Start()
    {
        //Assigning values before states
        harpoonRb = harpoon.GetComponent<Rigidbody2D>();
        harpoonOnHitEffect = harpoon.GetComponent<ParticleSystem>();
        harpoonTrail = harpoon.GetComponent<TrailRenderer>();
        garbageTags = garbageSpawner.GetItemsTags();
        defaultLayer = LayerMask.LayerToName(gameObject.layer);
        castCollisions = new List<RaycastHit2D>();


        //starting state
        currentState = inShipState;

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(HarpoonBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnterState(this, collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnTriggerEnterState(this, collision);
    }
}
