using UnityEngine;

public class PickUpTask : UnitTask
{
    public ICollectableResource CollectTarget { get; }

    public PickUpTask(ICollectableResource collectTarget)
    {
        CollectTarget = collectTarget;
    }
}
