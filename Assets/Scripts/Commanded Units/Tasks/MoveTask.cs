using UnityEngine;

public class MoveTask : UnitTask
{
    public Transform MoveTarget { get; }

    public MoveTask(Transform moveTarget)
    {
        MoveTarget = moveTarget;
    }
}
