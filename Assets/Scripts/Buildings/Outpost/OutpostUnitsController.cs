using System;
using System.Collections.Generic;
using UnityEngine;

public class OutpostUnitsController
{
    private List<UnitCollector> _workerUnits = new List<UnitCollector>();
    private UnitsCollectResourceBehaviour collectResourceBehaviour;

    public event Action UnitsChanged;
    public int UnitsCount => _workerUnits.Count;

    public void Initialize(Outpost outpost)
    {
        collectResourceBehaviour = new UnitsCollectResourceBehaviour(outpost);
    }

    public void UpdateBehaviour(List<ICollectableResource> aviableResources)
    {
        collectResourceBehaviour.SendToCollect(new List<UnitCollector>(_workerUnits), aviableResources);
    }

    public void AddUnit(UnitCollector unit)
    {
        _workerUnits.Add(unit);
        UnitsChanged?.Invoke();
    }
}
