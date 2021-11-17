using UnityEngine;
using UnityEngine.Events;

public class Admob_Reklam_Interstitial : MonoBehaviour
{
    private static Admob_Reklam_Interstitial instance;
    public static Admob_Reklam_Interstitial Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    [Header("Admob script :")]
    [SerializeField] private Admob_Reklam_Manager admob_Reklam_Manager;
    public UnityAction OnInterstitialClicked;
    public void SetInterstitial(Admob_Reklam_Manager admob_Reklam)
    {
        admob_Reklam_Manager = admob_Reklam;

        admob_Reklam_Manager.OnInitComplete += () => admob_Reklam_Manager.RequestInterstitialAd();

        admob_Reklam_Manager.OnInterstitialAdOpening += () => {
            RequestInterstitial();
            OnInterstitialClicked?.Invoke();
        }; 
        admob_Reklam_Manager.OnInterstitialAdLoaded += () => LoadInterstitial();

        transform.SetParent(admob_Reklam_Manager.transform);
    }
    // When click Interstitial button, request Interstitial Ad again
    public void RequestInterstitial()
    {
        admob_Reklam_Manager.RequestInterstitialAd();
    }
    // When load Interstitial Ad. open Interstitial button
    public void LoadInterstitial()
    {
        Canvas_Manager.Instance.OpenInterstitialButton();
    }
    // Add some function for clicked Interstitial
    public void WhenUseInterstitial(UnityAction action, bool isOne = true)
    {
        if (isOne)
        {
            OnInterstitialClicked = null;
        }
        OnInterstitialClicked += () => action();
    }
}