using UnityEngine;
using UnityEngine.Events;

public class Admob_Reklam_Banner : MonoBehaviour
{
    private static Admob_Reklam_Banner instance;
    public static Admob_Reklam_Banner Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    [Header("Admob script :")]
    [SerializeField] private Admob_Reklam_Manager admob_Reklam_Manager;
    public UnityAction OnBannerClicked;

    private void Start()
    {
        //show banner ad when admob sdk is initialized:
        admob_Reklam_Manager.OnInitComplete += () => admob_Reklam_Manager.ShowBannerAd();

        //When someone click banner ad.
        admob_Reklam_Manager.OnBannerAdOpening += () => {
            OnBannerClicked?.Invoke();
        };
    }
    public void SetBanner(Admob_Reklam_Manager admob_Reklam)
    {
        admob_Reklam_Manager = admob_Reklam;

        transform.SetParent(admob_Reklam_Manager.transform);
    }
    // Add some function for clicked banner
    public void WhenUseBanner(UnityAction action, bool isOne = true)
    {
        if (isOne)
        {
            OnBannerClicked = null;
        }
        OnBannerClicked += () => action();
    }
}