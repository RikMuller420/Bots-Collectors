using System;

public abstract class UnitTask
{
    private Action _performedDelegate;
    private Action _failedDelegate;

    public void SetPerformedDelegate(Action performedDelegate)
    {
        _performedDelegate = performedDelegate;
    }
    public void SetFailedDelegate(Action failedDelegate)
    {
        _failedDelegate = failedDelegate;
    }

    public void OnPerformed()
    {
        _performedDelegate?.Invoke();
    }

    public void OnFailed()
    {
        _failedDelegate?.Invoke();
    }
}
