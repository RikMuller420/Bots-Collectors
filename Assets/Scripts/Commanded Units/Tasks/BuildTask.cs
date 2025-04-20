using System;

public class BuildTask : UnitTask
{
    public BuildingFlag Flag { get; }
    public Action BuildIsDoneDelegate { get; }

    public BuildTask(BuildingFlag flag, Action buildIsDone)
    {
        Flag = flag;
        BuildIsDoneDelegate = buildIsDone;
    }
}
