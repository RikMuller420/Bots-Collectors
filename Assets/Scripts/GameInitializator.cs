using UnityEngine;

public class GameInitializator : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private MushroomGenerator _mushroomGenerator;
    [SerializeField] private Outpost _startOutpost;
    [SerializeField] private CollectorUnitGenerator _collectorUnitGenerator;

    private void Awake()
    {
        _game.Initialize(_mushroomGenerator, _startOutpost, _collectorUnitGenerator);
    }
}
