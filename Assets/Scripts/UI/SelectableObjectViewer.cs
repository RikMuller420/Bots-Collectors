using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObjectViewer : MonoBehaviour
{
    [SerializeField] private OutpostViewer _outpostViewer;

    private Dictionary<Type, ISelecatbleViewer> _selectablesViewers;
    private ISelecatbleViewer _activeViewer = null;

    public event Action ViewHided;

    private void Awake()
    {
        _selectablesViewers = new()
        {
            { typeof(Outpost),     _outpostViewer },
        };
    }

    public void ShowView(ISelectable selectable)
    {
        HideActiveViewers();

        if (_selectablesViewers.TryGetValue(selectable.GetType(), out ISelecatbleViewer viewer))
        {
            _activeViewer = viewer;
            viewer.Show(selectable);
            viewer.Hided += OnActiveViewHided;
        }
    }

    private void HideActiveViewers()
    {
        if(_activeViewer != null)
        {
            _activeViewer.Hide();
            _activeViewer.Hided -= OnActiveViewHided;
            _activeViewer = null;
        }
    }

    private void OnActiveViewHided()
    {
        ViewHided?.Invoke();
    }
}
