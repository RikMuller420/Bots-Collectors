using System;
using UnityEngine;

public class BuildingOutpostBehaviour : OutpostBehaviour
{
    private BuildingFlag _flag;
    private int _mushroomPerOutpost = 5;
    private bool _isBuildingStarted = false;
    private Action<UnitCollector, BuildingFlag> _buildIsDoneDelegate;

    public BuildingOutpostBehaviour(Outpost outpost, BuildingFlag flag, OutpostUnitsController unitsController,
                                    ResourceCoordinator resourceCoordinator) : base(outpost, unitsController, resourceCoordinator)
    {
        _flag = flag;
    }

    public override void OnResourceDetected()
    {
        TrySendUnitsToCollectResources();
    }

    public override void OnStorageUpdated()
    {
        TryBuildOutpost();
    }

    public override void OnUnitBecameFree()
    {
        TryBuildOutpost();
        TrySendUnitsToCollectResources();
    }

    public void UpdateBuildingInfo(Vector3 buildPosition, Action<UnitCollector, BuildingFlag> buildIsDone)
    {
        _flag.Activate();
        _flag.transform.position = buildPosition;
        _buildIsDoneDelegate = buildIsDone;
    }

    public void Deactivate()
    {
        _isBuildingStarted = false;
    }

    private void TryBuildOutpost()
    {
        if (_isBuildingStarted)
        {
            return;
        }

        if (Outpost.UnitsController.UnitsCount <= 1)
        {
            return;
        }

        if (Outpost.Storage.GetResourceAmount<Mushroom>() < _mushroomPerOutpost)
        {
            return;
        }

        if (UnitsController.IsAnyUnitFree)
        {
            if (Outpost.Storage.TryRemoveResources<Mushroom>(_mushroomPerOutpost))
            {
                _isBuildingStarted = true;
                UnitsController.SendFreeUnitToBuild(_flag, _buildIsDoneDelegate);
            }
        }
    }
}
