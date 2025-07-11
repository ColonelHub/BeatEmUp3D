using UnityEngine;
using Zenject;

public class GameBootstrapInstaller : MonoInstaller
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private EventBus eventBus;

    [Space]
    [SerializeField] private Player player;
    [SerializeField] private EnemySpawner enemySpawner;

    [Header("Effects")]
    [SerializeField] private ParticlePool hitParticlesPool;
    [SerializeField] private CameraShake cameraShake;

    public override void InstallBindings()
    {
        BindLevel();
        BindEnemySpawner();
        BindPlayer();
        BindEffects();
    }

    private void BindLevel()
    {
        Container.Bind<LevelManager>().FromInstance(levelManager).AsSingle().NonLazy();
        Container.Bind<EventBus>().FromInstance(eventBus).AsSingle().NonLazy();
    }
    private void BindEnemySpawner()
    {
        Container.Bind<EnemySpawner>().FromInstance(enemySpawner).AsSingle().NonLazy();
    }
    private void BindPlayer()
    {
        Container.Bind<Player>().FromInstance(player).AsSingle().NonLazy();
    }
    private void BindEffects()
    {
        Container.Bind<ParticlePool>().FromInstance(hitParticlesPool).WithConcreteId(ParticleType.Hit).NonLazy();
        Container.Bind<CameraShake>().FromInstance(cameraShake).AsSingle().NonLazy();
    }
}