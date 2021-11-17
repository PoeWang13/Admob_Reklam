using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
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
        Admob_Reklam_Banner.Instance.WhenUseBanner(BannerAdClicked);
        Admob_Reklam_Interstitial.Instance.WhenUseInterstitial(InterstitialButtonClicked);
        Admob_Reklam_Reward_Interstitial.Instance.WhenUseRewardInterstitial(GiveRewardInterstitial);
        Admob_Reklam_Reward.Instance.WhenUseReward(RewardButtonClicked);
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