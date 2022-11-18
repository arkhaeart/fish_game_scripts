using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stopwatch : MonoBehaviour
{
    public UnityEvent timerComplete;

    public void ToGame()
    {
        timerComplete?.Invoke();
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        MiniMapContinue.StartStopwatch += SetActive;
    }

    private void OnDestroy()
    {
        MiniMapContinue.StartStopwatch -= SetActive;
    }
}
