using System;
using UnityEngine.SceneManagement;

public class ContinuePopup : MainPopup
{
    public static Action ContinueGame;

    public override void Start()
    {
        base.Start();
    }

    public void OnContinue()
    {
        ContinueGame?.Invoke();
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
    }
}
