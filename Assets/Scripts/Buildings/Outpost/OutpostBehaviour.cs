using System.Collections.Generic;

public abstract class OutpostBehaviour : IOutpostBehaviour
{
    protected Outpost Outpost;
    protected OutpostUnitsController UnitsController;
    protected List<ICollectableResource> AviableResources;

    public OutpostBehaviour(Outpost outpost, OutpostUnitsController unitsController)
    {
        Outpost = outpost;
        UnitsController = unitsController;
        AviableResources = new List<ICollectableResource>();
    }

    public abstract void OnStorageUpdated();
    public abstract void OnUnitBecameFree();
    public abstract void OnResourceScanPerformed(List<ICollectableResource> aviableResources);

    protected void UpdateAviableResources()
    {
        for (int i = AviableResources.Count - 1; i >= 0; i--)
        {
            if (AviableResources[i].IsEnabled == false)
            {
                AviableResources.RemoveAt(i);
            }
        }
    }

    protected void TrySendUnitsToCollectResources()
    {
        if (UnitsController.IsAnyUnitFree)
        {
            UnitsController.SendFreeUnitsToCollect(new List<ICollectableResource>(AviableResources));
        }
    }
}
