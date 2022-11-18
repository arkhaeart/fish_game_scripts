using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public const string MENU = "Scenes/MainMenu";
    public const string FIRST_LEVEL = "Scenes/MeshLevels/Level-1";

    void Start()
    {
        Music.Pause();
    }

    public void ToNextScene()
    {
        if (PlayerPrefs.HasKey("FirstEnterance"))
        {
            Music.Play();
            SceneManager.LoadSceneAsync(MENU);
        }
        else
        {
            PlayerPrefs.SetInt("FirstEnterance", 1);
            Music.PlayGameMusic();
            SceneManager.LoadSceneAsync(FIRST_LEVEL);
        }
    }
}
