using System;
using UnityEngine;

public class OutpostInitializer : MonoBehaviour
{
    [SerializeField] private Outpost _outpost;
    [SerializeField] private OutpostViewer _outpostViewer;

    public void Initialize(MouseHandler mouseHandler, CollectorUnitGenerator collectorUnitGenerator,
                            OutpostBuilder outpostBuilder, ResourceCoordinator resourceCoordinator,
                            int collectorUnits = 0)
    {
        Action<Outpost> locateOutpostFlag = (outpost) => { mouseHandler.StartBuildBehaviour<Outpost>(outpost); };

        _outpost.Initialize(collectorUnitGenerator, outpostBuilder, locateOutpostFlag, resourceCoordinator);
        _outpostViewer.Initialize(_outpost);
        collectorUnitGenerator.SpawnCollectorUnit(_outpost, collectorUnits);
    }

    public void Initialize(Action<Outpost> locateOutpostFlag, CollectorUnitGenerator collectorUnitGenerator,
                            OutpostBuilder outpostBuilder, ResourceCoordinator resourceCoordinator, UnitCollector unit)
    {
        _outpost.Initialize(collectorUnitGenerator, outpostBuilder, locateOutpostFlag, resourceCoordinator);
        _outpostViewer.Initialize(_outpost);
        _outpost.UnitsController.AddUnit(unit);
    }
}
