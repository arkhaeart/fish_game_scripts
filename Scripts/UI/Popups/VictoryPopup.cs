//using Firebase.Analytics;
using GameSystems;
using Persistence;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class VictoryPopup : MainPopup
{
    [SerializeField] GameObject nextLevel;
    [SerializeField] Text popupText;
    [SerializeField] GameObject[] emptyStars;
    [SerializeField] GameObject[] fullStars;

    public static System.Action<LevelInfo, int> SaveProgress;

    public override void Start()
    {
        base.Start();

        LevelNumbers levelNumbers = new LevelNumbers(gameManager.currentLevelIndex, CreatedFrom.buildIndex);

        if (gameManager.data.levels[levelNumbers.ArrayIndex].stars < gameManager.currentConfig.lives)
        {
            LevelInfo newLevelInfo = new LevelInfo(true, gameManager.currentConfig.lives);
            SaveProgress?.Invoke(newLevelInfo, levelNumbers.ArrayIndex);
        }

        for (int i = 0; i < gameManager.currentConfig.lives; i++)
        {
            emptyStars[i].SetActive(false);
            fullStars[i].SetActive(true);
        }

        if (levelNumbers.ArrayIndex +1>=gameManager.numberOfScenes)
        {
            DisableButton();
        }

        //if(Firebase.FirebaseApp.DefaultInstance != null)
        //{
        //    FirebaseAnalytics.LogEvent("level", "completed", $"level-{levelNumbers.ArrayIndex} is completed");
        //}
    }

    private void DisableButton()
    {
        nextLevel.SetActive(false);
        popupText.text = "You have beaten the game!";
    }

    public void OnNextLevel()
    {
        gameManager.NextLevel();
    }
}
