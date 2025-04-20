using System.Collections.Generic;
using System.Linq;

public class CollectResourceTaskSetter : UnitsTaskSetter
{
    public CollectResourceTaskSetter(Outpost outpost) : base(outpost) { }

    public void SendToCollect(List<UnitCollector> units, List<ICollectableResource> aviableResources)
    {
        foreach (ICollectableResource resource in aviableResources)
        {
            if (resource.IsEnabled == false)
            {
                continue;
            }

            if (IsAnyUnitSendedToCollect(units, resource))
            {
                continue;
            }

            if (TryFindFreeUnit(units, out UnitCollector freeUnit))
            {
                SendUnitToCollect(freeUnit, resource);
            }
        }
    }

    private void SendUnitToCollect(UnitCollector worker, ICollectableResource resource)
    {
        PickUpTask pickUp = new PickUpTask(resource);
        BringCollectableTask backToBase = new BringCollectableTask(Outpost);

        worker.AddTask(pickUp);
        worker.AddTask(backToBase);
    }

    private bool IsAnyUnitSendedToCollect(List<UnitCollector> units, ICollectableResource resource)
    {
        foreach (UnitCollector worker in units)
        {
            bool isSendedToCollect = worker.Tasks
                .OfType<PickUpTask>()
                .Any(task => task.CollectTarget == resource);

            if (isSendedToCollect)
            {
                return true;
            }
        }

        return false;
    }
}
