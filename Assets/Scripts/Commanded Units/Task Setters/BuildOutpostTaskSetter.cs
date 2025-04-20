using System;
using System.Collections.Generic;
using System.Linq;

public class BuildOutpostTaskSetter : UnitsTaskSetter
{
    public BuildOutpostTaskSetter(Outpost outpost) : base(outpost) { }

    public void SendToBuild(List<UnitCollector> units, BuildingFlag target, Action<UnitCollector, BuildingFlag> buildIsDone)
    {
        if (IsAnyUnitSendedToBuild(units, target))
        {
            return;
        }

        if (TryFindFreeUnit(units, out UnitCollector freeUnit))
        {
            SendUnitToBuild(freeUnit, target, buildIsDone);
        }
    }

    private void SendUnitToBuild(UnitCollector worker, BuildingFlag target, Action<UnitCollector, BuildingFlag> buildIsDone)
    {
        Action buildIsDoneDelegate = () => buildIsDone?.Invoke(worker, target);

        BuildTask build = new BuildTask(target, buildIsDoneDelegate);
        worker.AddTask(build);
    }

    private bool IsAnyUnitSendedToBuild(List<UnitCollector> units, BuildingFlag target)
    {
        foreach (UnitCollector worker in units)
        {
            bool isSendedToBuild = worker.Tasks
                .OfType<BuildTask>()
                .Any(task => task.Flag == target);

            if (isSendedToBuild)
            {
                return true;
            }
        }

        return false;
    }
}
