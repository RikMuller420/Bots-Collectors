using System.Collections.Generic;

public abstract class UnitsTaskSetter
{
    protected Outpost Outpost;

    public UnitsTaskSetter(Outpost outpost)
    {
        Outpost = outpost;
    }

    protected bool TryFindFreeUnit(List<UnitCollector> units, out UnitCollector freeUnit)
    {
        freeUnit = null;

        foreach (UnitCollector worker in units)
        {
            if (worker.IsFree)
            {
                freeUnit = worker;

                return true;
            }
        }

        return false;
    }
}
