using Common;
using GameSystems;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PreloadMonoInstaller : MonoInstaller
{
    private const string MENU = "Scenes/MainMenu";
    private const string COMICS = "Scenes/ComicsScene";

    public override void InstallBindings()
    {
    }
    public override void Start()
    {
        Container.Resolve<GameManager>();
        StartCoroutine(Loading());
    }
    IEnumerator Loading()
    {
        AsyncOperation oper;

        if (PlayerPrefs.HasKey("FirstEnterance"))
        {
            oper = SceneManager.LoadSceneAsync(MENU, LoadSceneMode.Additive);
        }
        else
        {
            oper = SceneManager.LoadSceneAsync(COMICS, LoadSceneMode.Additive);
        }
        
        while (!oper.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(0);
    }
}