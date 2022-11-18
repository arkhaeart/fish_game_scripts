using Scriptables;
using UnityEngine;
using Zenject;

public class LevelMonoInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        QueueFor();
        Container.Bind(typeof(LevelManager)).AsSingle();
        Container.Bind(typeof(MiniMapHandler)).AsSingle();
        Container.Bind(typeof(Game)).AsSingle();
        Container.Bind(typeof(CheckPointManager)).AsSingle();
        Container.BindFactory<Fish.Settings,FishController, FishController.Factory>();
    }
    void QueueFor()
    {
        //Container.BindInstance(mainLight).WhenInjectedInto<MiniMapHandler>();
        //Container.BindInstance(miniMapCamera).WhenInjectedInto<MiniMapHandler>();
        //Container.BindInstance(miniMap).WhenInjectedInto<MiniMapHandler>();
        //Container.QueueForInject(mainLight);
        //Container.QueueForInject(miniMapCamera);
        //Container.QueueForInject(miniMap);

    }
    public override void Start()
    {
        Container.Resolve<LevelManager>();
        Container.Resolve<MiniMapHandler>();
        Container.Resolve<CheckPointManager>();
        Game game = Container.Resolve<Game>();
        game.Start();
    }
}