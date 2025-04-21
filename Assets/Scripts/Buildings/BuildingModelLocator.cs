using System;
using UnityEngine;

public class BuildingModelLocator : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private GameObject _modelRed;

    private float _maxRayDistance = 10000f;
    private Collider _ground;
    private LayerMask _layerMask;
    private Camera _camera;
    private Func<Vector2> _getCursorPosition;

    public Vector3 BuildPosition { get; private set; }
    public bool IsAbleToBuild { get; private set; }

    public void Initialize(Camera camera, Collider ground, LayerMask layerMask,
                            Func<Vector2> getCursorPosition)
    {
        _camera = camera;
        _ground = ground;
        _layerMask = layerMask;
        _getCursorPosition = getCursorPosition;
    }

    public void Activate()
    {
        UpdateModelPosition(_getCursorPosition());
        enabled = true;
    }

    public void Deactivate()
    {
        DeactivateModels();
        enabled = false;
    }

    public void UpdateModelPosition(Vector2 cursorPosition)
    {
        Ray ray = _camera.ScreenPointToRay(cursorPosition);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxRayDistance, _layerMask) == false)
        {
            DeactivateModels();
            IsAbleToBuild = false;

            return;
        }

        IsAbleToBuild = IsSuitableBuildPosition(hit);
        PlaceOutpostModel(hit.point, IsAbleToBuild);
        BuildPosition = hit.point;
    }

    private void DeactivateModels()
    {
        _model.SetActive(false);
        _modelRed.SetActive(false);
    }

    private void PlaceOutpostModel(Vector3 position, bool isAbleToBuild)
    {
        _model.SetActive(isAbleToBuild);
        _modelRed.SetActive(!isAbleToBuild);

        GameObject model = isAbleToBuild ? _model : _modelRed;
        model.transform.position = position;
    }

    private bool IsSuitableBuildPosition(RaycastHit hit)
    {
        if (hit.collider != _ground)
        {
            return false;
        }

        return true;
    }
}
