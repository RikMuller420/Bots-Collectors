using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BuildBehaviour : ClickBehaviour
{
    [SerializeField] private Collider _ground;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private BuildingModelLocator _outpostLocator;

    private BuildingModelLocator _locator;
    private Dictionary<Type, BuildingModelLocator> _buildingLocators = new();
    private Action<Vector3> _buildDelegate;
    private Action _endBehaviourDelegate;

    public void Initialize(Action endBehaviourDelegate, Func<Vector2> getCursorPosition)
    {
        base.Awake();
        _endBehaviourDelegate = endBehaviourDelegate;
        _outpostLocator.Initialize(Camera, _ground, _layerMask, getCursorPosition);

        _buildingLocators = new Dictionary<Type, BuildingModelLocator>()
        {
            { typeof(Outpost), _outpostLocator }
        };
    }

    public override void ProcessClick(Vector2 clickPosition)
    {
        if (_locator.IsAbleToBuild)
        {
            _buildDelegate?.Invoke(_locator.BuildPosition);
            Deactivate();
            _endBehaviourDelegate?.Invoke();
        }
    }

    public override void ProcessCursorMove(Vector2 cursorPosition)
    {
        _locator.UpdateModelPosition(cursorPosition);
    }

    public void Activate<T>(IBuildingOwner owner) where T : IBuildable
    {
        Type buildingType = typeof(T);
        _buildDelegate = (position) => owner.Build<T>(position);

        if (_buildingLocators.ContainsKey(buildingType))
        {
            _locator = _buildingLocators[buildingType];
        }

        _locator.Activate();
        enabled = true;
    }

    private void Deactivate()
    {
        _locator.Deactivate();
        enabled = false;
    }
}
