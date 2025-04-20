using UnityEngine;

public abstract class ClickBehaviour : MonoBehaviour
{
    protected Camera Camera;

    protected virtual void Awake()
    {
        Camera = Camera.main;
    }

    public abstract void ProcessClick();
}
