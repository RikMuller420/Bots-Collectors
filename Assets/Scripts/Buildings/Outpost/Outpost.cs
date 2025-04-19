using UnityEngine;

public class Outpost : MonoBehaviour, ISelectable
{
    [SerializeField] private ResourceScaner _baseScaner;
    [SerializeField] private Transform _topPoint;

    private Storage _storage;
    private OutpostUnitsController _unitsController;

    public Storage Storage => _storage;
    public OutpostUnitsController UnitsController => _unitsController;
    public Transform TopPoint => _topPoint;

    private void Awake()
    {
        _storage = new Storage();
        _unitsController = new OutpostUnitsController(this);
    }

    private void OnEnable()
    {
        _baseScaner.ResourceScanPerformed += _unitsController.UpdateBehaviour;
    }

    private void OnDisable()
    {
        _baseScaner.ResourceScanPerformed -= _unitsController.UpdateBehaviour;
    }
}
