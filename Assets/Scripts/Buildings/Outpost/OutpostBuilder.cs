using System;
using UnityEngine;

public class OutpostBuilder : MonoBehaviour
{
    [SerializeField] private OutpostInitializer _prefab;

    public void BuildOutpost(UnitCollector worker, Action<Outpost> locateFlagDelegate,
                            CollectorUnitGenerator collectorUnitGenerator, Vector3 position)
    {
        OutpostInitializer outpost = Instantiate(_prefab);
        outpost.transform.position = position;
        outpost.Initialize(locateFlagDelegate, collectorUnitGenerator, this, worker);
    }
}
