using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using Firebase.Analytics;

public class MainMenuManager : MonoBehaviour
{
    private static bool hasOpened = false;
    public GameObject main, levels;
    void Start()
    {
        LevelButton.OnLevelChosen = LoadLevel;
        Music.PlayMenuMusic();
        main.SetActive(true);
    }
    public void Init()
    {

    }
    public void ShowLevels()
    {//todo state machine etc
        main.SetActive(false);    
        levels.SetActive(true);
    }
    private void OnDisable()
    {
        LevelButton.OnLevelChosen = null;
    }
    void LoadLevel(int level)
    {
        StartCoroutine(Loading(level));
    }
    IEnumerator Loading(int level)
    {
        AsyncOperation oper = SceneManager.LoadSceneAsync(level);
        while(!oper.isDone)
        {
            yield return null;
        }
        SceneManager.UnloadSceneAsync(1);
    }
}
