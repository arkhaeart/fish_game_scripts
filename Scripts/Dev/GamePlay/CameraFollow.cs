using Scriptables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraFollow : MonoBehaviour
{
    Transform target;
    public Vector3 offset;
    new Camera camera;
    MyCour following,approaching;
    Settings settings;
    System.Action onApproach;
    Vector3 Target
    {
        get
        {
            if (target != null)
                return target.position+offset;
            else return Vector3.zero;
        }
    }
    [Inject]
    public void Init(Fish fish, GameSettings gameSettings)
    {
        target = fish.transform;
        settings = gameSettings.camera;
        camera = Camera.main;
        approaching = new MyCour(this, Approaching);
        following = new MyCour(this, Following);
        offset = settings.offset;
    }
    public void OnProceed(Camera cam, System.Action callback)
    {
        transform.position = cam.transform.position;
        camera.orthographicSize = cam.orthographicSize;
        Debug.Log("Camera Follow OnProceed");
        onApproach = callback;
        approaching.Run();
    }
    private void OnDisable()
    {
        //manager.OnGameStart -= StartGame;
    }
    IEnumerator Approaching()
    {
        
        while(Vector3.SqrMagnitude(transform.position-Target)>=0.5f||
            (camera.orthographicSize-(settings.OSApproachRatio * Time.deltaTime)) >= settings.baseOS)
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, Target, settings.approachingSpeed*Time.deltaTime);
            float newOrthSize = Mathf.Clamp(camera.orthographicSize - settings.OSApproachRatio * Time.deltaTime, settings.baseOS, 9999);
            camera.orthographicSize = newOrthSize;
            transform.position = newPos;
            yield return null;
        }
        camera.orthographicSize = settings.baseOS;
        OnApproached();
    }
    void OnApproached()
    {
        onApproach?.Invoke();
        following.Run();
    }
    IEnumerator Following()
    {
        while (true)
        {
            transform.position = Target;
            yield return null;
        }
    }
    [System.Serializable]
    public class Settings
    {
        public Vector3 offset;
        public float approachingSpeed;
        public float baseOS;
        public float OSApproachRatio;
    }
}
/*using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    MyCour following;
    private void Awake()
    {
        offset = target.position + transform.position;
        following = new MyCour(this, Following);
    }
    private void Start()
    {
        following.Run();
    }
    IEnumerator Following()
    {
        while (target != null)
        {
            transform.position = target.position + offset;
            yield return null;
        }
    }
}*/
