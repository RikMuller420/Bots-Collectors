using UnityEngine;

public class CollectorUnitGenerator : MonoBehaviour
{
    [SerializeField] private CollectorUnitPool _pool;

    public void CreateWorkerUnit(Outpost outpost, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            UnitCollector unit = _pool.Get();
            unit.transform.position = outpost.transform.position;
            outpost.UnitsController.AddUnit(unit);
        }
    }

}
