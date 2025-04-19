using UnityEngine;

public class UnitCollector : Unit
{
    [SerializeField] private Transform _pickUpHolder;

    protected override void Awake()
    {
        base.Awake();
        ResourcePickUpper pickUpper = new ResourcePickUpper(_pickUpHolder);

        AddTaskPerformer(typeof(PickUpTask), new PickUpTaskPerformer(Mover, pickUpper));
        AddTaskPerformer(typeof(BringCollectableTask), new BringCollectableTaskPerformer(Mover, pickUpper));
    }
}