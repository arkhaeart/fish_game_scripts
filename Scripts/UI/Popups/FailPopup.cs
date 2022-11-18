using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailPopup : MainPopup
{
    public GameObject reload, ad, modal;
    public float nextTryAfter = 0.8f;

    public override void Start()
    {
        base.Start();

        if (gameManager.currentConfig.lives > 0)
        {
            modal.SetActive(false);
            StartCoroutine(ReloadWithLives());
            return;
        }

        modal.SetActive(true);

        if(gameManager.RequireAds())
        {
            ShowAdsButton();
        }
    }

    IEnumerator ReloadWithLives()
    {
        yield return new WaitForSecondsRealtime(nextTryAfter);
        OnReload();
    }

    void ShowAdsButton()
    {
        reload.SetActive(false);
        ad.SetActive(true);
    }
    public void ContinueWithAd()
    {
        AdsManager.Instance.ShowRewardedAds(gameManager.ReloadAfterAds);
    }
}
