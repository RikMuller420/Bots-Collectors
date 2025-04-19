using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutpostViewer : MonoBehaviour, ISelecatbleViewer
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button _closeButton;
    [SerializeField] private TextMeshProUGUI _mushroomAmountText;
    [SerializeField] private TextMeshProUGUI _unitsAmountText;

    private Outpost _outpost = null;

    public event Action Hided;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Hide);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(Hide);
    }

    public void Show(ISelectable selectable)
    {
        Outpost outpost = selectable as Outpost;
        _outpost = outpost;
        _outpost.Storage.ResourcesAmountChanged += UpdateResourceView;
        _outpost.UnitsController.UnitsChanged += UpdateUnitsView;

        UpdateResourceView();
        UpdateUnitsView();

        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        if (_outpost != null)
        {
            _outpost.Storage.ResourcesAmountChanged -= UpdateResourceView;
            _outpost.UnitsController.UnitsChanged -= UpdateUnitsView;
            _outpost = null;
        }

        _canvasGroup.alpha = 0f;
        _canvasGroup.blocksRaycasts = false;
        Hided?.Invoke();
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
