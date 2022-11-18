using GameSystems;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MainPopup : MonoBehaviour
{
    private const string MAIN_MENU_PATH = "Scenes/MainMenu";

    protected GameManager gameManager;
    [Inject]
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    virtual public void Start()
    {
        Stop();
    }
    void Stop()
    {
        Time.timeScale = 0f;
    }
    void Continue()
    {
        Time.timeScale = 1f;
    }
    public void OnReload()
    {
        gameManager.ReloadLevel();
    }
    protected void OnDisable()
    {
        Continue();
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_PATH);
    }
}
