using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    public Text fps;
    public Text frameTime;
    public float timeCount;
    public float frameCount;
    public float refreshTime=0.3f;
    public float frameRate;
    void Update()
    {
        if (timeCount < refreshTime)
        {
            timeCount += Time.deltaTime;
            frameCount++;
        }
        else
        {
            frameRate = (float)frameCount / timeCount;
            frameCount = 0;
            timeCount = 0.0f;
            fps.text = frameRate.ToString();
        }
        frameTime.text = ((int)(Time.deltaTime * 1000)).ToString();
    }
}
