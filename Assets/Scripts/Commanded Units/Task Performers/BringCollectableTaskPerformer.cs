using UnityEngine;

public class BringCollectableTaskPerformer : TaskPerformer
{
    private ResourcePickUpper _pickUpper;

    public BringCollectableTaskPerformer(UnitMover mover, ResourcePickUpper pickUpper) : base(mover)
    {
        _pickUpper = pickUpper;
    }

    public override void Perform(UnitTask unitTask)
    {
        if (_pickUpper.IsResourceHolded == false)
        {
            unitTask.OnFailed();

            return;
        }

        BringCollectableTask task = unitTask as BringCollectableTask;
        Vector3 collectablePosition = task.Destination.transform.position;
        Mover.Move(collectablePosition);

        if (IsTargetReached(collectablePosition))
        {
            Bring(task);
        }
    }

    private void Bring(BringCollectableTask task)
    {
        _pickUpper.Give(task.Destination);
        task.OnPerformed();
    }
}
