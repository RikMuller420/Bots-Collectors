using UnityEngine;

public class MoveTask : UnitTask
{
    public Transform MoveTarget;

    public MoveTask(Transform moveTarget)
    {
        MoveTarget = moveTarget;
    }
}
