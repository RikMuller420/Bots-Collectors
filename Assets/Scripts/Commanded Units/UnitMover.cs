using UnityEngine;

public class UnitMover
{
    private Transform _unit;
    private float _speed;

    public UnitMover(Transform unit, float speed)
    {
        _unit = unit;
        _speed = speed;
    }

    public Transform Owner => _unit;

    public void Move(Vector3 position)
    {
        float step = _speed * Time.deltaTime;
        _unit.transform.position = Vector3.MoveTowards(_unit.transform.position, position, step);
        _unit.transform.LookAt(position);
    }
}
