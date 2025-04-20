using System;
using System.Collections.Generic;
using System.Linq;

public class OutpostUnitsController
{
    private List<UnitCollector> _workerUnits = new List<UnitCollector>();

    private CollectResourceTaskSetter _collectResourceTaskSetter;
    private BuildOutpostTaskSetter _buildOutpostTaskSetter;

    public event Action SomeUnitBecameFree;
    public event Action UnitsCountChanged;

    public int UnitsCount => _workerUnits.Count;
    public bool IsAnyUnitFree => _workerUnits.Any(worker => worker.IsFree);

    public OutpostUnitsController(Outpost outpost)
    {
        _collectResourceTaskSetter = new CollectResourceTaskSetter(outpost);
        _buildOutpostTaskSetter = new BuildOutpostTaskSetter(outpost);
    }

    public void AddUnit(UnitCollector unit)
    {
        _workerUnits.Add(unit);
        UnitsCountChanged?.Invoke();
        unit.TasksIsDone += UnitBecameFree;
    }

    public void RemoveUnit(UnitCollector unit)
    {
        _workerUnits.Remove(unit);
        UnitsCountChanged?.Invoke();
        unit.TasksIsDone -= UnitBecameFree;
    }

    public void SendFreeUnitsToCollect(List<ICollectableResource> aviableResources)
    {
        _collectResourceTaskSetter.SendToCollect(new List<UnitCollector>(_workerUnits), aviableResources);
    }

    public void SendFreeUnitToBuild(BuildingFlag target, Action<UnitCollector, BuildingFlag> buildIsDone)
    {
        _buildOutpostTaskSetter.SendToBuild(new List<UnitCollector>(_workerUnits), target, buildIsDone);
    }

    private void UnitBecameFree()
    {
        SomeUnitBecameFree?.Invoke();
    }
}
