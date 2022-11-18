using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MiniMapContinue : MonoBehaviour
{
    public System.Action OnClick;

    public static System.Action StartStopwatch;

    [Inject]
    public void Init(LevelManager manager)
    {
        manager.OnGameStart += Disable;
        StartStopwatch?.Invoke();
    }
    public void Clicked()
    {
        Debug.Log("Mini map continue clicked");
        OnClick?.Invoke();
        Disable();
    }
    void Disable()
    {
        gameObject.SetActive(false);
    }
}
