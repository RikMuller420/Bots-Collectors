using UnityEngine;

public interface ICollectableResource
{
    public void Activate();
    public void Deactivate();
    public void Destroy();

    public Transform Transform { get; }
    public bool IsEnabled { get; }
}
