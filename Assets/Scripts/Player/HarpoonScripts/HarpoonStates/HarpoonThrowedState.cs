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


    private void PlayHarpoonAnimation(Collider2D collision)
    {
        GameObject.Destroy(collision.gameObject.GetComponent<ParticleSystem>());
        stateManager.harpoonOnHitEffect.Play();
    }

    public override void OnCollisionEnterState(HarpoonStateManager stateManager, Collision2D collision)
    {
    }

    public override void OnTriggerEnterState(HarpoonStateManager stateManager, Collider2D collision)
    {
        if (collision.gameObject.tag == "Garbage" && stateManager.garbage == null)
        {
            PlayHarpoonAnimation(collision);
            stateManager.garbage = collision.gameObject;
            stateManager.garbage.transform.SetParent(stateManager.harpoon.transform);
            ObstaclesMoverScript.RemoveObstacle(stateManager.garbage);
        }
    }
}
