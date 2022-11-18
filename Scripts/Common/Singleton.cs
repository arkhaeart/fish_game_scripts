using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T: Singleton<T>
{
    static T instance;
    public static T Instance
    {
        get
        {
            if(instance==null)
            {
                Debug.LogError("Not initialised singleton requested!");
            }
            return instance;
        }
    }
    public Singleton()
    {
        instance = (T)this;
    }
}

