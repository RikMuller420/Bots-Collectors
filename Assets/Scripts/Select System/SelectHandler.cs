using UnityEngine;
using UnityEngine.InputSystem;

public class SelectHandler : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private SellectArrow _sellectArrow;
    [SerializeField] private SelectableObjectViewer _selectableViewer;

    private ISelectable _currentSelection;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _inputHandler.MouseClicked += TrySelect;
        _selectableViewer.ViewHided += OnViewHided;
    }
    private void OnDisable()
    {
        _inputHandler.MouseClicked -= TrySelect;
        _selectableViewer.ViewHided -= OnViewHided;
    }

    private void TrySelect()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out ISelectable selectable))
            {
                _selectableViewer.ShowView(selectable);
                _sellectArrow.TrackObject(selectable);
                _currentSelection = selectable;
            }
        }
    }

    private void OnViewHided()
    {
        _sellectArrow.Hide();
    }
}
