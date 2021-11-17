using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Canvas_Manager : MonoBehaviour
{
    private static Canvas_Manager instance;
    public static Canvas_Manager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public Button reklamInterstitialBut;
    public Button reklamRewardInterstitialBut;
    public Button reklamRewardBut;
    public Admob_Reklam_Manager admob_Reklam_Manager;
    // Use this function in a button for calling Interstitial Ad
    public void ClickInterstitial()
    {
        reklamInterstitialBut.interactable = false;
        admob_Reklam_Manager.ShowInterstitialAd();
    }
    public void OpenInterstitialButton()
    {
        reklamInterstitialBut.interactable = true;
    }
    // Use this function in a button for calling Reward Interstitial Ad
    public void ClickRewardInterstitial()
    {
        admob_Reklam_Manager.ShowRewardedInterstitialAd();
    }
    // Use this function in a button for calling Reward Ad
    public void ClickReward()
    {
        reklamRewardBut.interactable = false;
        admob_Reklam_Manager.ShowRewardAd();
    }
    public void OpenRewardButton()
    {
        reklamRewardBut.interactable = true;
    }
}