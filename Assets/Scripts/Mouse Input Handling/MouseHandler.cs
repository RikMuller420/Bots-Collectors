using System;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private SelectBehaviour _selectBehaviour;
    [SerializeField] private BuildBehaviour _buildBehaviour;

    private ClickBehaviour _clickBehaviour;

    private void Awake()
    {
        Func<Vector2> getCursorPosition = () => _inputHandler.MousePosition;
        _buildBehaviour.Initialize(ResetBehaviour, getCursorPosition);
        ResetBehaviour();
    }

    private void OnEnable()
    {
        _inputHandler.MouseClicked += ProcessClick;
        _inputHandler.MouseMoved += ProcessCursorMove;
    }

    private void OnDisable()
    {
        _inputHandler.MouseClicked -= ProcessClick;
        _inputHandler.MouseMoved -= ProcessCursorMove;
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

    private void ProcessClick(Vector2 clickPosition)
    {
        _clickBehaviour.ProcessClick(clickPosition);
    }

    private void ProcessCursorMove(Vector2 clickPosition)
    {
        _clickBehaviour.ProcessCursorMove(clickPosition);
    }
}
