using GameSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game 
{
    MiniMapHandler handler;
    LevelManager manager;
    GameManager gameManager;
    CheckPointManager checkPointManager;

    public Game(GameManager gameManager, MiniMapHandler handler,LevelManager manager,CheckPointManager checkPointManager)
    {
        this.checkPointManager = checkPointManager;
        this.gameManager = gameManager;
        this.handler = handler;
        this.manager = manager;

    }
    void CheckGameConfig()
    {
        bool fromCheckPoint = gameManager.CheckGameConfig(out CheckPointEntry entry);
        if(fromCheckPoint)
        {
            manager.StartFromCheckPoint(entry);
        }
    }
    public void Start()
    {
        CheckGameConfig();
        Zone.OnPlayerFail = () => End(false);
        WinZone.OnPlayerWon = () => End(true);
        handler.Run(manager.StartGame);
    }
    public void End(bool win)
    {
        if (win)
            gameManager.PlayerWon();
        else if (checkPointManager.TryGetCheckPoint(out CheckPointEntry entry))
        {
            gameManager.PlayerFailed(entry);
        }
        else gameManager.PlayerFailed();
    }

}
public class GameConfig
{
    public bool fromCheckPoint;
    public CheckPointEntry entry;
    public int lives;
    readonly int maxLives;

    public static System.Action<int> DecreaseLive;

    public GameConfig(int maxLives)
    {
        fromCheckPoint = false;
        this.maxLives = maxLives;
        lives = maxLives;
    }

    public void Update(CheckPointEntry entry)
    {
        fromCheckPoint = true;
        lives--;
        DecreaseLive?.Invoke(lives);
        this.entry = entry;
    }
}
