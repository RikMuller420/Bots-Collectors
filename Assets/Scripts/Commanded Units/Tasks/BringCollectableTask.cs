public class BringCollectableTask : UnitTask
{
    public Outpost Destination { get; }

    public BringCollectableTask(Outpost destination)
    {
        Destination = destination;
    }
}
