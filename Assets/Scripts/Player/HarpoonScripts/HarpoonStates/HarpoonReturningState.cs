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
        if (stateManager.garbage != null)
        {
            stateManager.harpoon.layer = LayerMask.NameToLayer(stateManager.returningLayer);
            stateManager.garbage.layer = LayerMask.NameToLayer(stateManager.returningLayer);
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


    public override void OnCollisionEnterState(HarpoonStateManager stateManager, Collision2D collision)
    { }
    public override void OnTriggerEnterState(HarpoonStateManager stateManager, Collider2D collision)
    {
        Debug.Log("Trigger");
        if (collision.gameObject.name == "HarpoonStart")
        {
            if (stateManager.garbage != null)
            {
                GameObject.Destroy(stateManager.garbage);
                stateManager.fixedJoint.connectedBody = null;
                stateManager.fixedJoint.enabled = false;
            }
            stateManager.harpoon.transform.position = stateManager.harpoonStartPoint.transform.position;
            stateManager.harpoonRb.bodyType = RigidbodyType2D.Static;
            stateManager.SwitchState(stateManager.inShipState);
        }
    }
}
