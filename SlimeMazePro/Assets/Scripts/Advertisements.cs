using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class Advertisements : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private string gameId = "4006652";
#elif UNITY_ANDROID
    private string gameId = "4006653";
#endif

    bool testMode = true;

    public string placementIdBanner = "bannerad1";

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);

        // this is the listener for the ad services
        Advertisement.AddListener(this);

        Advertisement.Initialize(gameId, testMode);

        // Run the banner as a coroutine after initialization is done for adverts
        StartCoroutine(ShowBannerWhenInitialized());
    }

    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Advertisement.Banner.SetPosition(BannerPosition.TOP_RIGHT);
        // Load banner using my placementID
        Advertisement.Banner.Show(placementIdBanner);
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Special code to stop your game or music
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(showResult == ShowResult.Finished)
        {
            Debug.Log("You're awarded with 10 coins!");
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Boo no reward");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.Log("The ad did not run");
        }
    }

    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
