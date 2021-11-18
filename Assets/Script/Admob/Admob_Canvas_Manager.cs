using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Admob_Canvas_Manager : MonoBehaviour
{
    private static Admob_Canvas_Manager instance;
    public static Admob_Canvas_Manager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    [SerializeField] private Button reklamInterstitialBut;
    [SerializeField] private Button reklamRewardBut;
    [SerializeField] private Admob_Reklam_Manager admob_Reklam_Manager;
    // Use this function in a button for calling Interstitial Ad
    public void ClickInterstitial()
    {
        if (reklamInterstitialBut != null)
        {
            reklamInterstitialBut.interactable = false;
        }
        admob_Reklam_Manager.ShowInterstitialAd();
    }
    public void OpenInterstitialButton()
    {
        if (reklamInterstitialBut != null)
        {
            reklamInterstitialBut.interactable = true;
        }
    }
    // Use this function in a button for calling Reward Interstitial Ad
    public void ClickRewardInterstitial()
    {
        admob_Reklam_Manager.ShowRewardedInterstitialAd();
    }
    // Use this function in a button for calling Reward Ad
    public void ClickReward()
    {
        if (reklamRewardBut != null)
        {
            reklamRewardBut.interactable = false;
        }
        admob_Reklam_Manager.ShowRewardAd();
    }
    public void OpenRewardButton()
    {
        if (reklamRewardBut != null)
        {
            reklamRewardBut.interactable = true;
        }
    }
}