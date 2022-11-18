using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : PersSingleton<AdsManager>, IUnityAdsListener
{
    System.Action<bool> OnAdFinished;

#if (UNITY_IOS)
    const string gameId = "3702938";
#elif (UNITY_ANDROID)
    const string gameId = "3702939";
#endif


    public void ShowRewardedAds(System.Action<bool> callback)
    {
        if (Advertisement.isInitialized && Advertisement.IsReady() && Advertisement.isSupported)
        {
            OnAdFinished = callback;
            Advertisement.Show();
        }
        else callback?.Invoke(false);
    }
    public void OnUnityAdsDidError(string message)
    {
        OnAdFinished?.Invoke(false);
        OnAdFinished = null;
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult== ShowResult.Finished)
        {
            OnAdFinished?.Invoke(true);
        }
        else
        {
            OnAdFinished?.Invoke(false);
        }
        OnAdFinished = null;
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ad started playing");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ads ready to show");
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Application.internetReachability!= NetworkReachability.NotReachable)
        {
            Advertisement.Initialize(gameId);
            Advertisement.AddListener(this);
        }

    }

}
