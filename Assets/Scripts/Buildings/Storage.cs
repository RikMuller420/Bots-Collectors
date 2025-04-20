using System;
using System.Collections.Generic;

public class Storage
{
    private Dictionary<Type, int> _resourcesAmounts;

    public event Action ResourcesAmountChanged;

    public Storage()
    {
        _resourcesAmounts = new Dictionary<Type, int>()
        {
            { typeof(Mushroom), 0 }
        };
    }

    public void AddResource(ICollectableResource resource)
    {
        Type resourceType = resource.GetType();

        if (_resourcesAmounts.ContainsKey(resourceType))
        {
            _resourcesAmounts[resourceType]++;
            ResourcesAmountChanged?.Invoke();
        }
    }

    public int GetResourceAmount<T>() where T : ICollectableResource
    {
        if (_resourcesAmounts.ContainsKey(typeof(T)))
        {
            return _resourcesAmounts[typeof(T)];
        }

        return 0;
    }

    public bool TryRemoveResources<T>(int amount) where T : ICollectableResource
    {
        if (amount < 0)
        {
            return false;
        }

        Type resourceType = typeof(T);

        if (_resourcesAmounts.ContainsKey(resourceType) == false)
        {
            return false;
        }

        if (_resourcesAmounts[resourceType] < amount)
        {
            return false;
        }

        _resourcesAmounts[resourceType] -= amount;

        return true;
    }
}
