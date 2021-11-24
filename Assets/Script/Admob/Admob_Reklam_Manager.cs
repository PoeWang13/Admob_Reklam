using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using UnityEngine.Events;
using System;

public class Admob_Reklam_Manager : MonoBehaviour
{
    [Header("Admob Ad Units :")]
    [SerializeField] private string idBanner = "";
    [SerializeField] private string idInterstitial = "";
    [SerializeField] private string idRewardedInterstitial = "";
    [SerializeField] private string idReward = "";
    private string testIdBanner = "ca-app-pub-3940256099942544/6300978111";
    private string testIdInterstitial = "ca-app-pub-3940256099942544/1033173712";
    private string testIdRewardedInterstitial = "ca-app-pub-3940256099942544/1033173712";
    private string testIdReward = "ca-app-pub-3940256099942544/5224354917";
    [Header("Activeted for using this Banners")]
    [SerializeField] private bool useBanner;
    [SerializeField] private bool useInterstitial;
    [SerializeField] private bool useRewardInterstitial;
    [SerializeField] private bool useReward;

    [HideInInspector] public BannerView AdBanner;
    [HideInInspector] public InterstitialAd AdInterstitial;
    [HideInInspector] public RewardedInterstitialAd AdRewardedInterstitial;
    [HideInInspector] public RewardedAd AdReward;

    public UnityAction OnInitComplete;

    private void Start()
    {
        RequestConfiguration requestConfiguration =
           new RequestConfiguration.Builder()
              .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
              .build();

        MobileAds.SetRequestConfiguration(requestConfiguration);

        MobileAds.Initialize(initstatus => {
            MobileAdsEventExecutor.ExecuteInUpdate(() => {
                if (OnInitComplete != null)
                    OnInitComplete.Invoke();
            });
        });
        if (useBanner)
        {
            Admob_Reklam_Banner banner = new GameObject("Banner_Reklam", typeof(Admob_Reklam_Banner)).GetComponent<Admob_Reklam_Banner>();
            banner.SetBanner(this);
            OnBannerAdOpening = null;
            OnBannerAdOpening += () => ShowBannerAd();
        }
        if (useInterstitial)
        {
            Admob_Reklam_Interstitial interstitial = new GameObject("Interstitial_Reklam", typeof(Admob_Reklam_Interstitial)).GetComponent<Admob_Reklam_Interstitial>();
            interstitial.SetInterstitial(this);
        }
        if (useRewardInterstitial)
        {
            Admob_Reklam_Reward_Interstitial interstitial = new GameObject("Reward_Interstitial_Reklam", typeof(Admob_Reklam_Reward_Interstitial)).GetComponent<Admob_Reklam_Reward_Interstitial>();
            interstitial.SetRewardInterstitial(this);
        }
        if (useReward)
        {
            Admob_Reklam_Reward reward = new GameObject("Reward_Reklam", typeof(Admob_Reklam_Reward)).GetComponent<Admob_Reklam_Reward>();
            reward.SetReward(this);
        }
    }
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
           .AddExtra("npa", PlayerPrefs.GetString("npa", "1"))
           .Build();
    }
    private void OnDestroy()
    {
        DestroyBannerAd();
        DestroyInterstitialAd();
        DestroyRewardedInterstitialAd();
    }
    #region Banner Reklam
    public UnityAction OnBannerAdOpening;
    public void ShowBannerAd()
    {
        AdBanner = new BannerView((idBanner != string.Empty) ? idBanner : testIdBanner, AdSize.Banner, AdPosition.Bottom);
        AdBanner.OnAdOpening += (sender, e) => {
            if (OnBannerAdOpening != null)
                OnBannerAdOpening.Invoke();
        };
        AdBanner.LoadAd(CreateAdRequest());
    }
    public void DestroyBannerAd()
    {
        if (AdBanner != null)
            AdBanner.Destroy();
    }
    public void HideBanner()
    {
        AdBanner.Hide();
    }
    public void ShowBanner()
    {
        AdBanner.Show();
    }
    #endregion

    #region Interstitial Ad
    public UnityAction OnInterstitialAdLoaded;
    public UnityAction OnInterstitialAdFailedToLoad;
    public UnityAction OnInterstitialAdOpening;
    public UnityAction OnInterstitialAdClosed;
    public void RequestInterstitialAd()
    {
        AdInterstitial = new InterstitialAd((idInterstitial != string.Empty) ? idInterstitial : testIdInterstitial);
        AdInterstitial.OnAdLoaded += (sender, e) => {
            if (OnInterstitialAdLoaded != null)
                OnInterstitialAdLoaded.Invoke();
        };
        AdInterstitial.OnAdFailedToLoad += (sender, e) => {
            if (OnInterstitialAdFailedToLoad != null)
                OnInterstitialAdFailedToLoad.Invoke();
        };
        AdInterstitial.OnAdOpening += (sender, e) => {
            if (OnInterstitialAdOpening != null)
                OnInterstitialAdOpening.Invoke();
        };
        AdInterstitial.OnAdClosed += (sender, e) => {
            if (OnInterstitialAdClosed != null)
                OnInterstitialAdClosed.Invoke();
        };
        AdInterstitial.LoadAd(CreateAdRequest());
    }
    public void ShowInterstitialAd()
    {
        if (AdInterstitial.IsLoaded())
        {
            AdInterstitial.Show();
        }
    }
    public void DestroyInterstitialAd()
    {
        if (AdInterstitial != null)
            AdInterstitial.Destroy();
    }
    #endregion

    #region Reward Interstitial Ad
    public UnityAction OnRewardInterstitialAdOpening;
    public void RequestRewardInterstitialAd()
    {
        RewardedInterstitialAd.LoadAd((idRewardedInterstitial != string.Empty) ? idRewardedInterstitial : testIdRewardedInterstitial, CreateAdRequest(), AdLoadCallback);
    }
    private void AdLoadCallback(RewardedInterstitialAd ad, AdFailedToLoadEventArgs error)
    {
        if (error == null)
        {
            AdRewardedInterstitial = ad;

            AdRewardedInterstitial.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresent;
            AdRewardedInterstitial.OnAdDidPresentFullScreenContent += HandleAdDidPresent;
            AdRewardedInterstitial.OnAdDidDismissFullScreenContent += HandleAdDidDismiss;
            AdRewardedInterstitial.OnPaidEvent += HandlePaidEvent;
        }
    }
    private void HandleAdFailedToPresent(object sender, AdErrorEventArgs args)
    {
        Debug.Log("Rewarded interstitial ad has failed to present.");
    }
    private void HandleAdDidPresent(object sender, EventArgs args)
    {
        Debug.Log("Ad Showed.");
    }
    private void HandleAdDidDismiss(object sender, EventArgs args)
    {
        Debug.Log("Rewarded interstitial ad has dismissed presentation.");
    }
    private void HandlePaidEvent(object sender, AdValueEventArgs args)
    {
        Debug.Log("Rewarded interstitial ad has received a paid event.");
    }
    public void ShowRewardedInterstitialAd()
    {
        if (AdRewardedInterstitial != null)
        {
            AdRewardedInterstitial.Show(UserEarnedRewardCallback);
        }
    }
    private void UserEarnedRewardCallback(Reward reward)
    {
        OnRewardInterstitialAdOpening?.Invoke();
    }
    public void DestroyRewardedInterstitialAd()
    {
        if (AdInterstitial != null)
            AdInterstitial.Destroy();
    }
    #endregion

    #region Reward Ad
    public UnityAction<Reward> OnRewardAdWatched;
    public UnityAction OnRewardAdLoaded;
    public UnityAction OnRewardAdFailedToLoad;
    public UnityAction OnRewardAdOpening;
    public UnityAction OnRewardAdClosed;
    public void RequestRewardAd()
    {
        AdReward = new RewardedAd((idReward != string.Empty) ? idReward : testIdReward);
        AdReward.OnAdClosed += (sender, e) => {
            if (OnRewardAdClosed != null)
                OnRewardAdClosed.Invoke();
        };
        AdReward.OnAdOpening += (sender, e) => {
            if (OnRewardAdOpening != null)
                OnRewardAdOpening.Invoke();
        };
        AdReward.OnAdFailedToLoad += (sender, e) => {
            if (OnRewardAdFailedToLoad != null)
                OnRewardAdFailedToLoad.Invoke();
        };
        AdReward.OnAdLoaded += (sender, e) => {
            if (OnRewardAdLoaded != null)
                OnRewardAdLoaded.Invoke();
        };
        AdReward.OnUserEarnedReward += (sender, reward) => {
            if (OnRewardAdWatched != null)
                OnRewardAdWatched.Invoke(reward);
        };
        AdReward.LoadAd(CreateAdRequest());
    }
    public void ShowRewardAd()
    {
        if (AdReward.IsLoaded())
        {
            AdReward.Show();
        }
    }
    #endregion
}