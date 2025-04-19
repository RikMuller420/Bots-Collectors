using UnityEngine;

public class PickUpTaskPerformer : TaskPerformer
{
    private ResourcePickUpper _pickUpper;

    public PickUpTaskPerformer(UnitMover mover, ResourcePickUpper pickUpper) : base(mover)
    {
        _pickUpper = pickUpper;
    }

    public override void Perform(UnitTask unitTask)
    {
        if (_pickUpper.IsResourceHolded)
        {
            unitTask.OnFailed();

            return;
        }

        PickUpTask task = unitTask as PickUpTask;

        if (task.CollectTarget.IsEnabled == false)
        {
            unitTask.OnFailed();

            return;
        }

        Vector3 collectablePosition = task.CollectTarget.Transform.position;
        Mover.Move(collectablePosition);

        if (IsTargetReached(collectablePosition))
        {
            PickUp(task);
        }
    }

    private void PickUp(PickUpTask task)
    {
        _pickUpper.PickUp(task.CollectTarget);
        task.OnPerformed();
    }
}
