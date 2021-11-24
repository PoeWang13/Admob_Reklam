using UnityEngine;
using System.Collections;

public class Admob_Reward_Functions : MonoBehaviour
{
    private static Admob_Reward_Functions instance;
    public static Admob_Reward_Functions Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Start()
    {
        StartCoroutine(AdmobOdulYukle());
    }
    IEnumerator AdmobOdulYukle()
    {
        yield return new WaitForSeconds(1);
        if (Admob_Reklam_Banner.Instance != null)
        {
            Admob_Reklam_Banner.Instance.WhenUseBanner(BannerAdClicked);
        }
        if (Admob_Reklam_Interstitial.Instance != null)
        {
            Admob_Reklam_Interstitial.Instance.WhenUseInterstitial(InterstitialButtonClicked);
        }
        if (Admob_Reklam_Reward_Interstitial.Instance != null)
        {
            Admob_Reklam_Reward_Interstitial.Instance.WhenUseRewardInterstitial(GiveRewardInterstitial);
        }
        if (Admob_Reklam_Reward.Instance != null)
        {
            Admob_Reklam_Reward.Instance.WhenUseReward(RewardButtonClicked);
        }
    }
    // When someone clicked Banner Ad, this function will active.
    public void BannerAdClicked()
    {
        Debug.Log("Banner Clicked.");
    }
    // When someone clicked Interstitial Ad, this function will active.
    public void InterstitialButtonClicked()
    {
        Debug.Log("Interstitial Clicked.");
    }
    // When someone clicked Reward Interstitial Ad, this function will active.
    public void GiveRewardInterstitial()
    {
        Debug.Log("Reward Interstitial Clicked.");
    }
    // When someone clicked Reward Ad, this function will active.
    public void RewardButtonClicked()
    {
        Debug.Log("Reward Clicked.");
    }
    // When someone watched Reward Ad, this function will active.
    public void GiveReward(int amount)
    {
        Debug.Log("Give Reward .");
    }
}