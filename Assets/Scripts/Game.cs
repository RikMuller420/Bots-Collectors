using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private int _startWorkerUnitCount = 3;

    private MushroomGenerator _mushroomGenerator;

    public void Initialize(MushroomGenerator mushroomGenerator, OutpostInitializer startOutpost,
                            CollectorUnitGenerator collectorUnitGenerator, OutpostBuilder outpostBuilder,
                            MouseHandler mouseHandler)
    {
        _mushroomGenerator = mushroomGenerator;
        startOutpost.Initialize(mouseHandler, collectorUnitGenerator, outpostBuilder, _startWorkerUnitCount);
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
