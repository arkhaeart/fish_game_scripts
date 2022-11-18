//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class InitFirebase : MonoBehaviour
//{
//    void Start()
//    {
//        DontDestroyOnLoad(this);

//        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
//            var dependencyStatus = task.Result;
//            if (dependencyStatus == Firebase.DependencyStatus.Available)
//            {

//                var app = Firebase.FirebaseApp.DefaultInstance;
//            }
//            else
//            {
//                UnityEngine.Debug.LogError(System.String.Format(
//                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
//            }
//        });
//    }

//    private void OnApplicationQuit()
//    {
//        Firebase.FirebaseApp.DefaultInstance.Dispose();
//        #if UNITY_IPHONE
//            Application.Quit();
//        #endif
//        #if UNITY_ANDROID
//            new AndroidJavaClass("java.lang.System").CallStatic("exit", 0);
//        #endif
//    }
//}
