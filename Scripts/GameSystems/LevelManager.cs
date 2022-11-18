using Scriptables;
using UnityEngine;

public class LevelManager
{
    public Fish fish;
    public FishController controller;
    public Light directionalLight;
    public event System.Action OnGameStart;

    public LevelManager(GameSettings settings, FishController.Factory factory,TurningControls controls)
    {
        controller = factory.Create(settings.fish);
        OnGameStart += controls.GameStart;
    }
    public void StartFromCheckPoint(CheckPointEntry entry)
    {
        Vector3 pos = entry.pos;
        Direction direction = entry.direction;
        controller.SetOnCheckPoint(pos, direction);
    }

    public void Init(Fish.Settings settings)
    {
        controller = new FishController(fish, settings);

    }
    public void StartGame()
    {
        controller.StartGame();
        OnGameStart?.Invoke();
    }
}
