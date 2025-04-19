using UnityEngine;

public class PickUpTask : UnitTask
{
    public ICollectableResource CollectTarget;

    public PickUpTask(ICollectableResource collectTarget)
    {
        CollectTarget = collectTarget;
    }
}
