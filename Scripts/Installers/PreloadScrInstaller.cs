using Common;
using GameSystems;
using Persistence;
using Scriptables;
using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PreloadScrInstaller", menuName = "Installers/PreloadScrInstaller")]
public class PreloadScrInstaller : ScriptableObjectInstaller<PreloadScrInstaller>
{
    public static bool usePlayerPrefs = true;
    public static int levelsCount = 12;
    private const string PLAYER_DATA = "PlayerData";

    public GameSettings gameSettings;
    public override void InstallBindings()
    {
        BindSettings();
        LoadPlayerData();
        Bind();
    }

    private void Bind()
    {
        Container.Bind(typeof(GameManager)).AsSingle();
        Container.Bind(typeof(AudioManager)).AsSingle();
    }

    private void LoadPlayerData() => Container.BindInstance( usePlayerPrefs ? LoadFromPlayerPrefs() : LoadFromFileSystem() );
    
    private PlayerData LoadFromFileSystem()
    {
        DataHandler.Init(DataHandlerCustomClass.GetDataHandlerInfo());
        return DataHandler.GetDataSync<PlayerData>();
    }
     
    private PlayerData LoadFromPlayerPrefs()
    {
        if (PlayerPrefs.HasKey(PLAYER_DATA))
        {
            return JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(PLAYER_DATA));
        }

        PlayerData playerData = new PlayerData();
        playerData.levels = new LevelInfo[levelsCount];

        for (int i = 0; i < levelsCount; i++)
        {
            playerData.levels[i] = new LevelInfo(false, 0);
        }

        return playerData;
    }

    private void BindSettings()
    {
        Container.BindInstance(gameSettings);
    }
}