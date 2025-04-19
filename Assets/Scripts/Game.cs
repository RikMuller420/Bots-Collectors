using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int startWorkerUnitCount = 3;

    private MushroomGenerator _mushroomGenerator;
    private Outpost _startOutpost;
    private CollectorUnitGenerator _collectorUnitGenerator;

    public void Initialize(MushroomGenerator mushroomGenerator, Outpost startOutpost, CollectorUnitGenerator collectorUnitGenerator)
    {
        _mushroomGenerator = mushroomGenerator;
        _startOutpost = startOutpost;
        _collectorUnitGenerator = collectorUnitGenerator;
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _mushroomGenerator.StartGenrate();
        _collectorUnitGenerator.CreateWorkerUnit(_startOutpost, startWorkerUnitCount);
    }
}
