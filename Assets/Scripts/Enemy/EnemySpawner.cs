using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [Inject] private DiContainer _diContainer;
    [Inject] private LevelManager _levelManager;
    [Inject] private EventBus _eventBus;
    [Inject] private GameUI _gameUI;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        _eventBus.OnEnemyDied += SpawnEnemyAtRandomPoint;
    }

    public void SpawnEnemyAtRandomPoint()
    {
        if (_levelManager.EnemiesLefs > 0)
        {
            SpawnEnemy(spawnPoints[Random.Range(0, spawnPoints.Length)].position);
            _levelManager.EnemiesLefs--;

            _gameUI.UpdateEnemiesLeft(_levelManager.EnemiesLefs + 1);
        }
        else
        {
            _levelManager.ProcessVictory();
        }
        
    }
    private void SpawnEnemy(Vector3 position)
    {
        GameObject enemy = _diContainer.InstantiatePrefab(enemyPrefab, position, Quaternion.identity, null);
    }

    private void OnDisable()
    {
        _eventBus.OnEnemyDied -= SpawnEnemyAtRandomPoint;
    }
}
