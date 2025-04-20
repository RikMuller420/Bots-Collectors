using System;
using System.Collections.Generic;
using UnityEngine;

public class Outpost : MonoBehaviour, ISelectable, IBuildable, IBuildingOwner
{
    [SerializeField] private ResourceScaner _resourceScaner;
    [SerializeField] private Transform _topPoint;
    [SerializeField] private BuildingFlag _flag;

    private Storage _storage;
    private OutpostUnitsController _unitsController;
    private CollectorUnitGenerator _collectorUnitGenerator;
    private OutpostBuilder _outpostBuilder;

    private IOutpostBehaviour _outpostBehaviour;
    private GeneratingUnitBehaviour _generatingUnitBehaviour;
    private BuildingOutpostBehaviour _buildingOutpostBehaviour;

    private Action<Outpost> _locateFlagDelegate;

    public Storage Storage => _storage;
    public OutpostUnitsController UnitsController => _unitsController;
    public Transform TopPoint => _topPoint;

    private void OnEnable()
    {
        _resourceScaner.ScanPerformed += OnResourceScanPerformed;
        _storage.ResourcesAmountChanged += OnStorageUpdated;
        _unitsController.SomeUnitBecameFree += OnUnitBecameFree;
    }

    private void OnDisable()
    {
        _resourceScaner.ScanPerformed -= OnResourceScanPerformed;
        _storage.ResourcesAmountChanged -= OnStorageUpdated;
        _unitsController.SomeUnitBecameFree -= OnUnitBecameFree;
    }

    public void Initialize(CollectorUnitGenerator collectorUnitGenerator, OutpostBuilder outpostBuilder,
                            Action<Outpost> locateFlagDelegate)
    {
        _storage = new Storage();
        _unitsController = new OutpostUnitsController(this);
        _collectorUnitGenerator = collectorUnitGenerator;
        _locateFlagDelegate = locateFlagDelegate;
        _outpostBuilder = outpostBuilder;
        _generatingUnitBehaviour = new GeneratingUnitBehaviour(this, collectorUnitGenerator, _unitsController);
        _buildingOutpostBehaviour = new BuildingOutpostBehaviour(this, _flag, _unitsController);
        _outpostBehaviour = _generatingUnitBehaviour;

        enabled = true;
    }

    public void OnSelected()
    {
        _locateFlagDelegate?.Invoke(this);
    }

    private void OnStorageUpdated()
    {
        _outpostBehaviour.OnStorageUpdated();
    }

    private void OnResourceScanPerformed(List<ICollectableResource> aviableResources)
    {
        _outpostBehaviour.OnResourceScanPerformed(aviableResources);
    }

    private void OnUnitBecameFree()
    {
        _outpostBehaviour.OnUnitBecameFree();
    }

    public void Build<T>(Vector3 buildPosition) where T : IBuildable
    {
        if (typeof(T) == typeof(Outpost))
        {
            if (UnitsController.UnitsCount <= 1)
            {
                return;
            }

            _buildingOutpostBehaviour.UpdateBuildingInfo(buildPosition, BuildOutpost);
            _outpostBehaviour = _buildingOutpostBehaviour;
        }
    }

    private void BuildOutpost(UnitCollector worker, BuildingFlag flag)
    {
        UnitsController.RemoveUnit(worker);
        _outpostBuilder.BuildOutpost(worker, _locateFlagDelegate,
                                    _collectorUnitGenerator, flag.transform.position);
        flag.Deactivate();
        _buildingOutpostBehaviour.Deactivate();
        _outpostBehaviour = _generatingUnitBehaviour;
    }
}
