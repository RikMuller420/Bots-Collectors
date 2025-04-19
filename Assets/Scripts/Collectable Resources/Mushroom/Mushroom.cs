using UnityEngine;

public class Mushroom : PoolableObject, ICollectableResource
{
    public Transform Transform => transform;
    public bool IsEnabled => enabled;

    public void Activate()
    {
        enabled = true;
    }

    public void Deactivate()
    {
        enabled = false;
    }

    public void Destroy()
    {
        transform.parent = null;
        OnDeactivated();
    }
}
