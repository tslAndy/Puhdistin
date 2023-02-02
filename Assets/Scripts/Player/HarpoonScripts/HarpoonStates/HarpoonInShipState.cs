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

        stateManager.harpoon.layer = LayerMask.NameToLayer(stateManager.defaultLayer);
        stateManager.harpoon.tag = "Harpoon";
    }

    public override void UpdateState(HarpoonStateManager stateManager)
    {
        stateManager.harpoon.transform.position = stateManager.harpoonStartPoint.transform.position;

        if (Input.GetButtonDown("Fire1") && stateManager.harpoonRb.bodyType == RigidbodyType2D.Static)
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(clickPosition.y - stateManager.harpoon.transform.position.y, 
                                      clickPosition.x - stateManager.harpoon.transform.position.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, -60, 30);
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector2.right;  

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
