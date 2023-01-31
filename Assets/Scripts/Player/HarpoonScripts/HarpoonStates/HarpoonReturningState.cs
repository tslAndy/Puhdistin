using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonReturningState : HarpoonBaseState
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

        if (stateManager.garbage != null)
        {
            stateManager.harpoon.layer = LayerMask.NameToLayer(stateManager.returningLayer);
            stateManager.garbage.layer = LayerMask.NameToLayer(stateManager.returningLayer);
        } else
        {
            stateManager.harpoon.layer = LayerMask.NameToLayer(stateManager.returningLayer);
        }
        Vector2 direction = (stateManager.harpoonStartPoint.transform.position - stateManager.harpoon.transform.position).normalized;

        ReturnHarpoon(direction);

        if (!Input.GetMouseButton(1))
        {
            stateManager.SwitchState(stateManager.throwedState);
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

        int count = stateManager.harpoonRb.Cast(
            direction,
            stateManager.movmentFilter,
            stateManager.castCollisions,
            stateManager.returnSpeed * Time.deltaTime + stateManager.collisionOffset
        );

        if (count == 0 || count == 1)
        {
            stateManager.harpoonRb.velocity = stateManager.returnSpeed * direction;
            return true;
        }
        else
        {
            return false;
        }
    }

    //Attaches garbage to harpoon
    public override void OnCollisionEnterState(HarpoonStateManager stateManager, Collision2D collision)
    { 
       
    }

    //Checks is it a start harpoon point or garbage
    public override void OnTriggerEnterState(HarpoonStateManager stateManager, Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Garbage") && stateManager.garbage == null)
        {
            Debug.Log("Trigger");
            stateManager.throwedState.OnTriggerEnterState(stateManager, collision);
            stateManager.SwitchState(stateManager.throwedState);
        }

        if (collision.gameObject.name == "HarpoonStart")
        {
            if (stateManager.garbage != null)
            {
                //Collecting garbage, destroying it, adiing poits
                stateManager.onGarbageCollecting.HandleCollect(stateManager.garbage.tag, stateManager.garbage);
                GameObject.Destroy(stateManager.garbage);
            }
            stateManager.harpoon.transform.position = stateManager.harpoonStartPoint.transform.position;
            stateManager.harpoonRb.bodyType = RigidbodyType2D.Static;
            stateManager.SwitchState(stateManager.inShipState);
        }
    }
}
