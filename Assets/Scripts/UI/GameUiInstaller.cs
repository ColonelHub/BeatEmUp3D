using UnityEngine;
using Zenject;

public class GameUiInstaller : MonoInstaller
{
    [SerializeField] private GameUI gameUI;
    public override void InstallBindings()
    {
        Container.Bind<GameUI>()
            .FromInstance(gameUI)
            .AsSingle()
            .NonLazy();
    }
}