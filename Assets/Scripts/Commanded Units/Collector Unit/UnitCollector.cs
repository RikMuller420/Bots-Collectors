using UnityEngine;

public class UnitCollector : Unit
{
    [SerializeField] private Transform _pickUpHolder;

    protected override void Awake()
    {
        base.Awake();
        ResourcePickUpper pickUpper = new ResourcePickUpper(_pickUpHolder);

        AddTaskPerformer<PickUpTask>(new PickUpTaskPerformer(Mover, pickUpper));
        AddTaskPerformer<BringCollectableTask>(new BringCollectableTaskPerformer(Mover, pickUpper));
        AddTaskPerformer<BuildTask>(new BuildTaskPerformer(Mover));
    }
}