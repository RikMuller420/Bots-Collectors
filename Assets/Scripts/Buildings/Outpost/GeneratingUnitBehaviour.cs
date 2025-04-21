public class GeneratingUnitBehaviour : OutpostBehaviour
{
    private CollectorUnitGenerator _collectorUnitGenerator;
    private int _mushroomPerUnit = 3;

    public GeneratingUnitBehaviour(Outpost outpost, CollectorUnitGenerator collectorUnitGenerator,
                                OutpostUnitsController unitsController, ResourceCoordinator resourceCoordinator) :
                                base(outpost, unitsController, resourceCoordinator)
    {
        _collectorUnitGenerator = collectorUnitGenerator;
    }

    public override void OnResourceDetected()
    {
        TrySendUnitsToCollectResources();
    }

    public override void OnStorageUpdated()
    {
        TryBuildUnit();
    }

    public override void OnUnitBecameFree()
    {
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
