using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public void OnPause()
    {
        SceneManager.LoadSceneAsync("Scenes/PauseScene", LoadSceneMode.Additive);
    }
}
