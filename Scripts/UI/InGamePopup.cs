using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGamePopup : MonoBehaviour
{
    [SerializeField] GameObject nextLevel;
    [SerializeField] Text popupText;

/*    public static Action ContinueGame;
    public static Action StopGame;*/

/*    private void Start()
    {
        StopGame?.Invoke();
    }*/

    /*public void OnContinue()
    {
        ContinueGame?.Invoke();
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
    }*/

    public void OnNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int numberOfScenes = SceneManager.sceneCountInBuildSettings;

        if (nextSceneIndex == numberOfScenes)
        {

        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
    }

    /*public void OnReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void ToMenu() 
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    } */
}
