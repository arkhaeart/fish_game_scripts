using Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MiniMapHandler 
{
    Light light;
    Camera miniMapCam;
    GameObject miniMap;
    Settings settings;
    MyCour fading;
    MiniMapContinue @continue;
    CameraFollow cameraFollow;
    //MiniMapContinue @continue;
    public MiniMapHandler([Inject(Id ="mini")]Light light , [Inject(Id = "mini")] Camera miniMapCam, 
        [Inject(Id = "mini")] RectTransform miniMap, GameSettings settings, CourMonoHelper courHelper, MiniMapContinue @continue,
        CameraFollow cameraFollow)
    {
        this.light = light;
        this.miniMapCam = miniMapCam;
        this.miniMap = miniMap.gameObject;
        this.settings = settings.miniMap;
        this.cameraFollow = cameraFollow;
        this.@continue = @continue;
        fading = new MyCour(courHelper, Fading);


    }
    public void Run(System.Action callback)
    {
        @continue.OnClick = () =>
        {
            cameraFollow.OnProceed(miniMapCam,callback);
            fading.Run();

        };

    }
    IEnumerator Fading()
    {
        Stop();
        while(light.intensity-settings.lightFadeRate*Time.deltaTime>=0)
        {
            light.intensity -= settings.lightFadeRate*Time.deltaTime;
            yield return null;
        }
        light.intensity = 0f;
        
    }
    void Stop()
    {
        miniMap.SetActive(false);
        miniMapCam.gameObject.SetActive(false);
        
    }
    [System.Serializable]
    public class Settings
    {
        public float showMapTime = 3;
        public float lightFadeRate = 0.5f;
    }
}
