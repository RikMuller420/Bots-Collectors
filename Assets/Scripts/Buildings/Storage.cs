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

    public int GetResourceAmount(Type resourceType)
    {
        if (_resourcesAmounts.ContainsKey(resourceType))
        {
            return _resourcesAmounts[resourceType];
        }

        return 0;
    }
}
