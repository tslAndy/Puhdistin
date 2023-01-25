using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonInShipState : HarpoonBaseState 
{
    private HarpoonStateManager stateManager;


    public override void EnterState(HarpoonStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public override void UpdateState(HarpoonStateManager stateManager)
    {
        stateManager.harpoon.transform.position = stateManager.harpoonStartPoint.transform.position;

        if (Input.GetButtonDown("Fire1") && stateManager.harpoonRb.bodyType == RigidbodyType2D.Static)
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (clickPosition - stateManager.harpoonRb.transform.position).normalized;
            stateManager.harpoonRb.bodyType = RigidbodyType2D.Dynamic;
            stateManager.harpoonRb.velocity = direction * stateManager.speed;
            Debug.Log(direction * stateManager.speed * stateManager.harpoonStartPoint.transform.localScale.x);
            stateManager.SwitchState(stateManager.throwedState);
        }

    }

    public override void OnCollisionEnterState(HarpoonStateManager stateManager, Collision2D collision)
    {
    }

    public override void OnTriggerEnterState(HarpoonStateManager stateManager, Collider2D collision)
    {

    }
}
