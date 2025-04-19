using UnityEngine;

public class Outpost : MonoBehaviour, ISelectable
{
    [SerializeField] private ResourceScaner _baseScaner;
    [SerializeField] private Transform _topPoint;

    private Storage _storage = new Storage();
    private OutpostUnitsController _unitsController = new OutpostUnitsController();

    public Storage Storage => _storage;
    public OutpostUnitsController UnitsController => _unitsController;
    public Transform TopPoint => _topPoint;

    private void Awake()
    {
        _unitsController.Initialize(this);
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
