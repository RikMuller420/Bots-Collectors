public abstract class OutpostBehaviour : IOutpostBehaviour
{
    protected Outpost Outpost;
    protected OutpostUnitsController UnitsController;

    private ResourceCoordinator _resourceCoordinator;

    public OutpostBehaviour(Outpost outpost, OutpostUnitsController unitsController,
                            ResourceCoordinator resourceCoordinator)
    {
        Outpost = outpost;
        UnitsController = unitsController;
        _resourceCoordinator = resourceCoordinator;
    }

    public abstract void OnStorageUpdated();
    public abstract void OnUnitBecameFree();
    public abstract void OnResourceDetected();

    protected void TrySendUnitsToCollectResources()
    {
        if (UnitsController.IsAnyUnitFree)
        {
            UnitsController.SendFreeUnitsToCollect(_resourceCoordinator);
        }
    }
}
