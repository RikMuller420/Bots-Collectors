using UnityEngine;
using UnityEngine.InputSystem;

public class SelectBehaviour : ClickBehaviour
{
    [SerializeField] private SellectArrow _sellectArrow;

    public override void ProcessClick()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out ISelectable selectable))
            {
                _sellectArrow.TrackObject(selectable);
                selectable.OnSelected();

                return;
            }
        }

        HideArrow();
    }

    public void HideArrow()
    {
        _sellectArrow.Hide();
    }
}
