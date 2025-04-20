using UnityEngine;

public class SellectArrow : MonoBehaviour
{
    private Transform _target;
    private float _verticalOffset = 10f;

    private void Update()
    {
        transform.position = _target.position + new Vector3(0, _verticalOffset, 0);
    }

    public void TrackObject(ISelectable selectable)
    {
        _target = selectable.TopPoint;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
