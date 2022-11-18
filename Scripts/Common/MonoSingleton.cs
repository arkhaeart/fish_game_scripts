using UnityEngine;


/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : MonoSingleton<MyClassName> {}
/// </summary>
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    const bool createInstanceIfNotExist = true;
    static T instance; 

    public static T Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<T>();
                if (instance == null && createInstanceIfNotExist) {
                    GameObject singleton = new GameObject(typeof(T).Name);
                    instance = singleton.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake() {
        if (instance == null)
        {
            instance = this as T;
        }
        else 
        {
            Destroy(gameObject);
        }
    }

}