using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScaner : MonoBehaviour
{
    private const int MaxResourceCount = 32;

    [SerializeField] private float _reloadTime = 1f;
    [SerializeField] private float _radius = 150f;

    private Collider[] _overlapBuffer = new Collider[MaxResourceCount];
    private WaitForSeconds _wait;

    public event Action<List<ICollectableResource>> ScanPerformed;

    private void Start()
    {
        _wait = new WaitForSeconds(_reloadTime);
        StartCoroutine(ScanRepeatedly());
    }

    private IEnumerator ScanRepeatedly()
    {
        while (enabled)
        {
            yield return _wait;

            List<ICollectableResource> resources = GetResourcesInArea();
            ScanPerformed?.Invoke(resources);
        }
    }

    private List<ICollectableResource> GetResourcesInArea()
    {
        List<ICollectableResource> resources = new List<ICollectableResource>();

        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _overlapBuffer);

        for (int i = 0; i < hitCount; i++)
        {
            if (_overlapBuffer[i].TryGetComponent(out ICollectableResource resource))
            {
                if (resource.IsEnabled)
                {
                    resources.Add(resource);
                }
            }
        }

        return resources;
    }
}
