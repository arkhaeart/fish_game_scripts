using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicsButton : MonoBehaviour
{
    public void ShowComics()
    {
        SceneManager.LoadSceneAsync("Scenes/ComicsScene");
    }
}
