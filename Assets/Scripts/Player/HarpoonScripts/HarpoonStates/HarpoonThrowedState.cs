using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonThrowedState : HarpoonBaseState
{
    private HarpoonStateManager stateManager;
    public override void EnterState(HarpoonStateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public override void UpdateState(HarpoonStateManager stateManager)
    {
        if (Vector3.Distance(stateManager.harpoonStartPoint.transform.position, stateManager.harpoon.transform.position) < 0.5 && stateManager.garbage != null)
        {
            stateManager.harpoon.layer = LayerMask.NameToLayer(stateManager.defaultLayer);
        }

        Vector3 pos = Camera.main.WorldToScreenPoint(stateManager.harpoon.transform.position);

        bool outOfBounds = !Screen.safeArea.Contains(pos);
        if (outOfBounds) stateManager.SwitchState(stateManager.returningState);


        //if harpoon in the ship, we desabling trail, otherwise enabling
        if (stateManager.harpoonStartPoint.transform.position == stateManager.harpoon.transform.position)
        {
            stateManager.harpoonTrail.enabled = false;
            stateManager.harpoon.transform.position = stateManager.harpoonStartPoint.transform.position;
        }
        else
        {
            stateManager.harpoonTrail.enabled = true;
        }

        if (Input.GetMouseButton(1))
        {
            stateManager.SwitchState(stateManager.returningState);
        }

    }


    private void PlayHarpoonAnimation(Collision2D collision)
    {
        GameObject.Destroy(collision.gameObject.GetComponent<ParticleSystem>());
        stateManager.harpoonOnHitEffect.Play();
    }

    public override void OnCollisionEnterState(HarpoonStateManager stateManager, Collision2D collision)
    {
        if (Array.Exists(stateManager.garbageTags, element => element == collision.gameObject.tag) && stateManager.garbage == null)
        {

            PlayHarpoonAnimation(collision);
            stateManager.garbage = collision.gameObject;
            stateManager.fixedJoint.enabled = true;
            stateManager.fixedJoint.connectedBody = stateManager.garbage.GetComponent<Rigidbody2D>();
            stateManager.onGarbageCollecting.HandleCollect(stateManager.garbage.tag);
        }
    }

    public override void OnTriggerEnterState(HarpoonStateManager stateManager, Collider2D collision)
    {

    }
}
