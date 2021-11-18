using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Admob_Reklam_Reward : MonoBehaviour
{
    private static Admob_Reklam_Reward instance;
    public static Admob_Reklam_Reward Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    [Header("Admob script :")]
    [SerializeField] private Admob_Reklam_Manager admob_Reklam_Manager;

    [Header("Reward amount :")]
    [SerializeField] private int rewardAmount;
    public UnityAction OnRewardClicked;

    public void SetReward(Admob_Reklam_Manager admob_Reklam)
    {
        admob_Reklam_Manager = admob_Reklam;
        admob_Reklam_Manager.OnInitComplete += () => admob_Reklam_Manager.RequestRewardAd();

        admob_Reklam_Manager.OnRewardAdOpening += () => {
            RequestReward();
            OnRewardClicked?.Invoke();
        };
        admob_Reklam_Manager.OnRewardAdLoaded += OpenRewardButton;
        admob_Reklam_Manager.OnRewardAdWatched += OnRewardAdWatchedHandle;

        transform.SetParent(admob_Reklam_Manager.transform);
    }
    // When click Reward button, request Reward ad again
    public void RequestReward()
    {
        admob_Reklam_Manager.RequestRewardAd();
    }
    // When load Reward Ad. open Reward button
    private void OpenRewardButton()
    {
        Admob_Canvas_Manager.Instance.OpenRewardButton();
    }
    // When someone watch Reward Ad.
    private void OnRewardAdWatchedHandle(GoogleMobileAds.Api.Reward reward)
    {
        Admob_Reward_Functions.Instance.GiveReward((int)reward.Amount);
    }
    // Add some function for clicked Reward
    public void WhenUseReward(UnityAction action, bool isOne = true)
    {
        if (isOne)
        {
            OnRewardClicked = null;
        }
        OnRewardClicked += () => action();
    }
}