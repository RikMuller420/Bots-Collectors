using System.Collections.Generic;

public interface IOutpostBehaviour
{
    public void OnStorageUpdated();
    public void OnUnitBecameFree();
    public void OnResourceDetected();
}
