using UnityEngine;

public abstract class TaskPerformer
{
    protected UnitMover Mover;

    private float _reachTreshold = 1f;

    public TaskPerformer(UnitMover mover)
    {
        Mover = mover;
    }

    public abstract void Perform(UnitTask task);

    protected bool IsTargetReached(Vector3 targetPosition)
    {
        return (Mover.Owner.position - targetPosition).sqrMagnitude < _reachTreshold;
    }
}
