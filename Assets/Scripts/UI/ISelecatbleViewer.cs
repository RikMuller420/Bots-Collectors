using System;

public interface ISelecatbleViewer 
{
    public event Action Hided;

    public void Show(ISelectable selectable);
    public void Hide();
}
