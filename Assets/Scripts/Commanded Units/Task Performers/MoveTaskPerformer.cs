public class MoveTaskPerformer : TaskPerformer
{
    public MoveTaskPerformer(UnitMover mover) : base(mover) { }

    public override void Perform(UnitTask unitTask)
    {
        MoveTask task = unitTask as MoveTask;
        Mover.Move(task.MoveTarget.position);

        if (IsTargetReached(task.MoveTarget.position))
        {
            unitTask.OnPerformed();
        }
    }
}
