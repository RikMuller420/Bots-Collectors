using UnityEngine;

public class ResourcePickUpper
{
    private Transform _holder;
    private ICollectableResource _holdedResource;

    public ResourcePickUpper(Transform holder)
    {
        _holder = holder;
    }

    public bool IsResourceHolded => _holdedResource != null;

    public void PickUp(ICollectableResource resource)
    {
        _holdedResource = resource;

        resource.Transform.parent = _holder;
        resource.Transform.localPosition = Vector3.zero;
        resource.Deactivate();
    }

    public void Give(Outpost destination)
    {
        destination.Storage.AddResource(_holdedResource);
        _holdedResource.Destroy();

        _holdedResource = null;
    }
}
