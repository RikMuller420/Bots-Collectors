using UnityEngine;

public class GameInitializator : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private MushroomGenerator _mushroomGenerator;
    [SerializeField] private OutpostInitializer _startOutpost;
    [SerializeField] private CollectorUnitGenerator _collectorUnitGenerator;
    [SerializeField] private MouseHandler _mouseHandler;
    [SerializeField] private OutpostBuilder _outpostBuilder;

    private void Awake()
    {
        _game.Initialize(_mushroomGenerator, _startOutpost, _collectorUnitGenerator,
                        _outpostBuilder, _mouseHandler);
    }
}
