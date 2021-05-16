using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    private static AdManager instance;
    public static AdManager Instance { get { return instance; } }

    [SerializeField] private string gameID;
    [SerializeField] private string rewardedVideoPlacementId;
    [SerializeField] private bool testMode;

    private void Awake()
    {
        instance = this;
        Advertisement.Initialize(gameID, testMode);
    }

    public void ShowRewardedAd()
    {
        ShowOptions so = new ShowOptions();
        Advertisement.Show(rewardedVideoPlacementId, so);
    }
}
