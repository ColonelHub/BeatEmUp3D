using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [HideInInspector] public int EnemiesLefs;

    [Inject] private EnemySpawner _enemySpawner;
    [Inject] private GameUI _gameUI;
    [Inject] private EventBus _eventBus;

    [SerializeField] private LevelsData levelsData;
    [SerializeField] private float levelStartDelay = 3f;

    private int _level;

    private void Awake()
    {
        _level = PlayerPrefs.GetInt("CurrentLevel", 1);
        EnemiesLefs = levelsData.Data[_level - 1].EnemiesInLevel;
    }

    private void Start()
    {
        DOVirtual.DelayedCall(levelStartDelay, StartLevel);

        _eventBus.OnPlayerDied += ProcessDefeat;
        _gameUI.UpdateEnemiesLeft(EnemiesLefs);
    }

    private void StartLevel()
    {
        _enemySpawner.SpawnEnemyAtRandomPoint();
    }

    public void ProcessVictory()
    {
        _eventBus.InvokeGameEnded();
        _gameUI.ShowVictoryScreen();

        _gameUI.UpdateEnemiesLeft(0);

        int lastAvailableLevel = PlayerPrefs.GetInt("LastAvailableLevel", 1);

        if (_level + 1 > lastAvailableLevel && _level + 1 != SceneManager.sceneCount)
        {
            PlayerPrefs.SetInt("LastAvailableLevel", _level + 1);
        }
    }

    private void ProcessDefeat()
    {
        _eventBus.InvokeGameEnded();
        _gameUI.ShowDefeatScreen();
    }

    private void OnDisable()
    {
        _eventBus.OnPlayerDied -= ProcessDefeat;
    }
}
