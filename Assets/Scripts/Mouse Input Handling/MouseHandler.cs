using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private SelectBehaviour _selectBehaviour;
    [SerializeField] private BuildBehaviour _buildBehaviour;

    private ClickBehaviour _clickBehaviour;

    private void Awake()
    {
        _buildBehaviour.Initialize(ResetBehaviour);
        ResetBehaviour();
    }

    private void OnEnable()
    {
        _inputHandler.MouseClicked += ProcessClick;
    }

    private void OnDisable()
    {
        _inputHandler.MouseClicked -= ProcessClick;
    }

    private void ProcessClick()
    {
        _clickBehaviour.ProcessClick();
    }

    public void StartBuildBehaviour<T>(IBuildingOwner owner) where T : IBuildable
    {
        _buildBehaviour.Activate<T>(owner);
        _clickBehaviour = _buildBehaviour;
    }

    public void ResetBehaviour()
    {
        _selectBehaviour.HideArrow();
        _clickBehaviour = _selectBehaviour;
    }
}
