using UnityEngine;
using UnityEngine.Events;

public class Admob_Reklam_Reward_Interstitial : MonoBehaviour
{
    private static Admob_Reklam_Reward_Interstitial instance;
    public static Admob_Reklam_Reward_Interstitial Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    [Header("Admob script :")]
    [SerializeField] private Admob_Reklam_Manager admob_Reklam_Manager;
    public UnityAction OnRewardInterstitialClicked;
    public void SetRewardInterstitial(Admob_Reklam_Manager admob_Reklam)
    {
        admob_Reklam_Manager = admob_Reklam;

        admob_Reklam_Manager.OnInitComplete += () => admob_Reklam_Manager.RequestRewardInterstitialAd();

        admob_Reklam_Manager.OnRewardInterstitialAdOpening += () =>
        {
            RequestRewardInterstitial();
            OnRewardInterstitialClicked?.Invoke();
        };
        //admob_Reklam_Manager.OnInterstitialAdLoaded += () => LoadRewardInterstitial();

        transform.SetParent(admob_Reklam_Manager.transform);
    }
    // When click Reward Interstitial button, request Reward Interstitial Ad again
    public void RequestRewardInterstitial()
    {
        admob_Reklam_Manager.RequestRewardInterstitialAd();
    }
    // Add some function for clicked Reward Interstitial
    public void WhenUseRewardInterstitial(UnityAction action, bool isOne = true)
    {
        if (isOne)
        {
            OnRewardInterstitialClicked = null;
        }
        OnRewardInterstitialClicked += () => action();
    }
}