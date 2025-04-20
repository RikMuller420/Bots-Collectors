using UnityEngine;

public interface IBuildingOwner
{
    public void Build<T>(Vector3 buildPosition) where T : IBuildable;
}
