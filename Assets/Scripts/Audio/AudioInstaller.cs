using UnityEngine;
using Zenject;

public class AudioInstaller : MonoInstaller
{
    [SerializeField] private AudioManager audioManager;
    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().FromInstance(audioManager).AsSingle().NonLazy();
    }
}
