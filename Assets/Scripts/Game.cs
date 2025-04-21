using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int _startWorkerUnitCount = 3;

    private MushroomGenerator _mushroomGenerator;
    private ResourceCoordinator _resourceCoordinator;

    public void Initialize(MushroomGenerator mushroomGenerator, OutpostInitializer startOutpost,
                            CollectorUnitGenerator collectorUnitGenerator, OutpostBuilder outpostBuilder,
                            MouseHandler mouseHandler)
    {
        _resourceCoordinator = new ResourceCoordinator();
        _mushroomGenerator = mushroomGenerator;
        startOutpost.Initialize(mouseHandler, collectorUnitGenerator, outpostBuilder,
                                _resourceCoordinator, _startWorkerUnitCount);
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _mushroomGenerator.StartGenrate();
    }
}
