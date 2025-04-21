using System;
using System.Collections.Generic;

public class ResourceCoordinator
{
    private List<ICollectableResource> _freeResources = new();
    private List<ICollectableResource> _busyResources = new();

    public event Action ResourceDetected;

    public void SubscribeScaner(ResourceScaner scaner)
    {
        scaner.ScanPerformed += ResourceScanned;
    }

    public void UnsubscribeScaner(ResourceScaner scaner)
    {
        scaner.ScanPerformed -= ResourceScanned;
    }

    public List<ICollectableResource> AviableResources()
    {
        return new List<ICollectableResource>(_freeResources);
    }

    public void ReserveResource(ICollectableResource resource)
    {
        if (_freeResources.Contains(resource))
        {
            _freeResources.Remove(resource);
            _busyResources.Add(resource);
        }
    }

    public void ReleaseBusyResource(ICollectableResource resource)
    {
        if (_busyResources.Contains(resource))
        {
            _busyResources.Remove(resource);
        }
    }

    private void ResourceScanned(List<ICollectableResource> resources)
    {
        foreach (ICollectableResource resource in resources)
        {
            if (_freeResources.Contains(resource))
            {
                continue;
            }

            if (_busyResources.Contains(resource))
            {
                continue;
            }

            _freeResources.Add(resource);
            ResourceDetected?.Invoke();
        }
    }
}
