using UnityEngine;

public interface ISelectable
{
    public Transform TopPoint { get; }
    public void OnSelected();
}
