using System.Collections.Generic;
using System.Linq;

public class UnitsCollectResourceBehaviour
{
    private Outpost _outpost;

    public UnitsCollectResourceBehaviour(Outpost outpost)
    {
        _outpost = outpost;
    }

    public void SendToCollect(List<UnitCollector> units, List<ICollectableResource> aviableResources)
    {
        foreach (ICollectableResource resource in aviableResources)
        {
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

    private bool TryFindFreeUnit(List<UnitCollector> units, out UnitCollector freeUnit)
    {
        freeUnit = null;

        foreach (UnitCollector worker in units)
        {
            if (worker.IsFree)
            {
                freeUnit = worker;

                return true;
            }
        }

        return false;
    }

    private void SendUnitToCollect(UnitCollector worker, ICollectableResource resource)
    {
        PickUpTask pickUp = new PickUpTask(resource);
        BringCollectableTask backToBase = new BringCollectableTask(_outpost);

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
