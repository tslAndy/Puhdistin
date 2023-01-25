using UnityEngine;

public abstract class HarpoonBaseState
{
    public abstract void EnterState(HarpoonStateManager stateManager);

    public abstract void UpdateState(HarpoonStateManager stateManager);

    public abstract void OnCollisionEnterState(HarpoonStateManager stateManager, Collision2D collision);
    public abstract void OnTriggerEnterState(HarpoonStateManager stateManager, Collider2D collision);

}
