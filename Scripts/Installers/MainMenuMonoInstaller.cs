using Persistence;
using UnityEngine;
using Zenject;

public class MainMenuMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindFactories();
        Container.Bind<AudioManager>().AsSingle();
    }
    void BindFactories()
    {
        /*Container.BindFactory<LevelNumbers, LevelInfo, LevelButton, LevelButton.Factory>().
            FromComponentInNewPrefab(levelButtonPrefab);*/
        Container.Bind(typeof(int), typeof(LevelInfo)).AsSingle();
    }
}