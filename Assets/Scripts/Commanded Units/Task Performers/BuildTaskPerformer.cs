using UnityEngine;

public class BuildTaskPerformer : TaskPerformer
{
    public BuildTaskPerformer(UnitMover mover) : base(mover) { }

    public override void Perform(UnitTask unitTask)
    {
        BuildTask task = unitTask as BuildTask;

        Vector3 collectablePosition = task.Flag.transform.position;
        Mover.Move(collectablePosition);

        if (IsTargetReached(collectablePosition))
        {
            Build(task);
        }
    }

    private void Build(BuildTask task)
    {
        task.BuildIsDoneDelegate?.Invoke();
        task.OnPerformed();
    }
}
