using TMPro;
using UnityEngine;

public class OutpostViewer : MonoBehaviour
{
    [SerializeField] private Outpost _outpost;
    [SerializeField] private TextMeshProUGUI _mushroomAmountText;
    [SerializeField] private TextMeshProUGUI _unitsAmountText;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _outpost.Storage.ResourcesAmountChanged += UpdateResourceView;
        _outpost.UnitsController.UnitsChanged += UpdateUnitsView;
    }

    private void OnDisable()
    {
        _outpost.Storage.ResourcesAmountChanged -= UpdateResourceView;
        _outpost.UnitsController.UnitsChanged -= UpdateUnitsView;
    }

    private void Update()
    {
        transform.LookAt(_mainCamera.transform);
    }

    private void UpdateResourceView()
    {
        _mushroomAmountText.text = _outpost.Storage.GetResourceAmount(typeof(Mushroom)).ToString();
    }

    private void UpdateUnitsView()
    {
        _unitsAmountText.text = _outpost.UnitsController.UnitsCount.ToString();
    }
}
