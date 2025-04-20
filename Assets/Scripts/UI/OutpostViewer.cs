using TMPro;
using UnityEngine;

public class OutpostViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _mushroomAmountText;
    [SerializeField] private TextMeshProUGUI _unitsAmountText;

    private Outpost _outpost;
    private Camera _mainCamera;

    private void OnEnable()
    {
        _outpost.Storage.ResourcesAmountChanged += UpdateResourceView;
        _outpost.UnitsController.UnitsCountChanged += UpdateUnitsView;
    }

    private void OnDisable()
    {
        _outpost.Storage.ResourcesAmountChanged -= UpdateResourceView;
        _outpost.UnitsController.UnitsCountChanged -= UpdateUnitsView;
    }

    private void Update()
    {
        transform.LookAt(_mainCamera.transform);
    }

    public void Initialize(Outpost outpost)
    {
        _outpost = outpost;
        _mainCamera = Camera.main;
        enabled = true;
    }

    private void UpdateResourceView()
    {
        _mushroomAmountText.text = _outpost.Storage.GetResourceAmount<Mushroom>().ToString();
    }

    private void UpdateUnitsView()
    {
        _unitsAmountText.text = _outpost.UnitsController.UnitsCount.ToString();
    }
}
