using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;

    protected UnitMover Mover;

    private UnitTask _currentTask;
    private List<UnitTask> _tasks = new List<UnitTask>();
    private Dictionary<Type, TaskPerformer> _tasksPerformers = new();

    public event Action TasksIsDone;

    public bool IsFree => _tasks.Count == 0;
    public ReadOnlyCollection<UnitTask> Tasks => _tasks.AsReadOnly();

    protected virtual void Awake()
    {
        Mover = new UnitMover(transform, _speed);
        AddTaskPerformer<MoveTask>(new MoveTaskPerformer(Mover));
    }

    private void FixedUpdate()
    {
        PerformTasksBehaviour();
    }

    public void AddTask(UnitTask task)
    {
        _tasks.Add(task);
    }

    protected void AddTaskPerformer<T>(TaskPerformer taskPerformer) where T : UnitTask
    {
        _tasksPerformers.Add(typeof(T), taskPerformer);
    }

    private void PerformTasksBehaviour()
    {
        if (_currentTask != null)
        {
            PerformTask(_currentTask);

            return;
        }

        if (_tasks.Count == 0)
        {
            return;
        }

        AssignCurrentTask(_tasks[0]);
    }

    private void AssignCurrentTask(UnitTask task)
    {
        _currentTask = task;
        _currentTask.AddPerformedDelegate(RemoveCurrentTask);
        _currentTask.AddFailedDelegate(RemoveCurrentTask);
    }

    private void RemoveCurrentTask()
    {
        _tasks.Remove(_currentTask);
        _currentTask = null;

        if (_tasks.Count == 0)
        {
            TasksIsDone?.Invoke();
        }
    }

    private void PerformTask(UnitTask task)
    {
        if (_tasksPerformers.TryGetValue(task.GetType(), out TaskPerformer taskPerformer))
        {
            taskPerformer.Perform(task);
        }
    }
}
