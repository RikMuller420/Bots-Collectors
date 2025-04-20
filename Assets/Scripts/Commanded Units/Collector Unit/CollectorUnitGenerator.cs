using UnityEngine;

public class CollectorUnitGenerator : MonoBehaviour
{
    [SerializeField] private UnitCollector _prefab;

    public void SpawnCollectorUnit(Outpost outpost, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            UnitCollector unit = Instantiate(_prefab);
            unit.transform.position = outpost.transform.position;
            outpost.UnitsController.AddUnit(unit);
        }
    }
}
