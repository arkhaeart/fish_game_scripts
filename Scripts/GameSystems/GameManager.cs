using Scriptables;
using Persistence;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameSystems
{
    public class GameManager
    {
        //GameSettings settings;
        public PlayerData data;
        public GameConfig currentConfig;
        public const int utilScenes = 7;
        public const int maxLives = 3;//value - 1 = number of attempts without ads
        public int currentLevelIndex;
        System.Action actionOnNextSceneLoad;
        public int numberOfScenes;
        const string victoryKey = "Scenes/VictoryScene";
        const string failKey = "Scenes/FailScene";

       
        public GameManager( PlayerData data)
        {
            Debug.Log("GAME MANAGER BEGIN");
            Debug.Log($"Game manager initialising" +
                $"\nPlayerData: {data}");

            //this.settings = settings;
            this.data = data;
            
            SceneManager.sceneLoaded += SceneLoaded;

            VictoryPopup.SaveProgress = ChangePlayerData;

            DevPanel.ChangeData = ChangePlayerData_dev;
            currentConfig = new GameConfig(maxLives);
            numberOfScenes = SceneManager.sceneCountInBuildSettings-utilScenes;
        }
        public bool CheckGameConfig(out CheckPointEntry entry)
        {
            if(currentConfig.fromCheckPoint)
            {
                entry = currentConfig.entry;
                return true;
            }
            else
            {
                entry = null;
                return false;
            }
        }
        public void PlayerWon()
        {
            LoadMenu(victoryKey);
        }
        public void PlayerFailed()
        {
            LoadMenu(failKey);
        }
        public void PlayerFailed(CheckPointEntry entry)
        {
            currentConfig.Update(entry);
            LoadMenu(failKey);
        }
        public bool RequireAds()
        {
            return currentConfig.lives <= 0;
        }
        public void NextLevel()
        {
            currentConfig = new GameConfig(maxLives);
            SceneManager.LoadScene(currentLevelIndex + 1);
        }
        public void ReloadAfterAds(bool finished)
        {
            if(finished)
            {
                currentConfig.lives = maxLives;
                ReloadLevel();
            }
            else
            {
                currentConfig = new GameConfig(maxLives);
                ReloadLevel();
            }
        }
        public void ReloadLevel()
        {
            SceneManager.LoadScene(currentLevelIndex);
        }

        void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            int buildIndex = scene.buildIndex;
            if (buildIndex >= utilScenes)
            {
                currentLevelIndex = buildIndex;
            }
            else if (buildIndex == 1)
            {
                currentConfig = new GameConfig(maxLives);
            }
            actionOnNextSceneLoad?.Invoke();
            actionOnNextSceneLoad = null;
        }

        void ChangePlayerData(LevelInfo newLevelInfo, int levelIndex)
        {
            data.levels[levelIndex] = newLevelInfo;

            if (PreloadScrInstaller.usePlayerPrefs)
            {
                PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(data));
            }
            else
            {
                DataHandler.SaveNewDataInFileAsync(data);
            }
            
        }

        void ChangePlayerData_dev(PlayerData newData)
        {
            data = newData;
        }
        void LoadMenu(string id)
        {
            SceneManager.LoadScene(id, LoadSceneMode.Additive);
        }
    }
}
