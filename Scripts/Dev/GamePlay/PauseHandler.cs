using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler 
{
    const float pauseRatio = 0.05f;
    const float pauseStep = 0.01f;
    WaitForSecondsRealtime wait;
    MyCour pausing;
    public PauseHandler(Fish fish)
    {
        pausing = new MyCour(fish, Pausing);
        wait = new WaitForSecondsRealtime(pauseStep);
    //    FishController.HandleInputOn += (ds,d) => pausing.Run();
    //    FishController.HandleInputOff += UnPausing;
    }
    IEnumerator Pausing()
    {
        while (Time.timeScale-pauseRatio >= 0) 
        {
            yield return wait;
            Time.timeScale -= pauseRatio;
        }
        Time.timeScale = 0;
    }
    void UnPausing()
    {
        pausing.Stop();
        Time.timeScale = 1;
    }
}
