using System.Collections.Generic;

public class GeneratingUnitBehaviour : OutpostBehaviour
{
    private CollectorUnitGenerator _collectorUnitGenerator;
    private int _mushroomPerUnit = 3;

    public GeneratingUnitBehaviour(Outpost outpost, CollectorUnitGenerator collectorUnitGenerator,
                                OutpostUnitsController unitsController) : base(outpost, unitsController)
    {
        _collectorUnitGenerator = collectorUnitGenerator;
    }

    public override void OnResourceScanPerformed(List<ICollectableResource> aviableResources)
    {
        AviableResources = aviableResources;
        TrySendUnitsToCollectResources();
    }

    public override void OnStorageUpdated()
    {
        TryBuildUnit();
    }

    public override void OnUnitBecameFree()
    {
        UpdateAviableResources();
        TrySendUnitsToCollectResources();
    }

    private void TryBuildUnit()
    {
        if (Outpost.Storage.GetResourceAmount<Mushroom>() < _mushroomPerUnit)
        {
            return;
        }

        if (Outpost.Storage.TryRemoveResources<Mushroom>(_mushroomPerUnit))
        {
            _collectorUnitGenerator.SpawnCollectorUnit(Outpost);
        }
    }
}
