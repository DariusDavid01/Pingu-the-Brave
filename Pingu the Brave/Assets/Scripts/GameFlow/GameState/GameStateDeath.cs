using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStateDeath : GameState, IUnityAdsListener
{
    //public static GameStateDeath Instance { get { return instance; } }
    //private static GameStateDeath instance;
    public GameObject deathUI;
    [SerializeField] private TextMeshProUGUI highscore;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI fishTotal;
    [SerializeField] private TextMeshProUGUI currentFish;

    //completion circle fields
    [SerializeField] private Image completionCircle;
    public float timeToDecision = 2.5f;

    private float deathTime;
    private void OnEnable()
    {
        Advertisement.AddListener(this);
    }

    private void OnDisable()
    {
        Advertisement.RemoveListener(this);
    }
    /*private void Awake()
     {
         //DontDestroyOnLoad(this);
         //instance = this;
         //DontDestroyOnLoad(completionCircle);
     }*/
    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.motor.PausePlayer();
        deathTime = Time.time;

        
            deathUI.SetActive(true);
        System.Random rnd = new System.Random();
        int x = rnd.Next(6, 8);
        Debug.Log(x);
        if (x == 6)
        {
            GameObject rev = GameObject.FindGameObjectWithTag("revive");
            if (rev != null)
            {   
                rev.GetComponent<Image>().enabled = false;
                rev.GetComponent<Button>().enabled = false;
            //rev.GetComponentInChildren<Image>().enabled = false;
            Image[] images = rev.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                img.enabled = false;
            } }
        }
        else
        {
            GameObject rev = GameObject.FindGameObjectWithTag("revive");
            rev.GetComponent<Image>().enabled = true;
            rev.GetComponent<Button>().enabled = true;
            //rev.GetComponentInChildren<Image>().enabled = true;
            Image[] images = rev.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                img.enabled = true;
            }
        }

        //prior to saving, set the highscore if needed
        if (SaveManager.Instance.save.Highscore < (int)GameStats.Instance.score)
        {
            SaveManager.Instance.save.Highscore = (int)GameStats.Instance.score;
            currentScore.color = Color.green;
            if (GameManager.Instance.isConnectedToGooglePlayServices)
            {
                Social.ReportScore(SaveManager.Instance.save.Highscore, GPGSIds.leaderboard_tabel_de_scor, (succes) =>
                {
                    if (!succes) Debug.LogError("Unable to post highscore");
                });
            }
        }
        else
            currentScore.color = Color.white;

        SaveManager.Instance.save.Fish += GameStats.Instance.fishCollectedThisSession;
        SaveManager.Instance.Save();

        highscore.text = "Cel mai mare scor : " + SaveManager.Instance.save.Highscore;
        currentScore.text = GameStats.Instance.ScoreToText();
        fishTotal.text = "Total pestisori : " + SaveManager.Instance.save.Fish;
        currentFish.text = GameStats.Instance.FishToText();
    }
    public override void Destruct()
    {
        deathUI.SetActive(false);
        //PlayerMotor motor = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerMotor>();
        //motor.anim.SetTrigger("Idle");
    }
    public override void UpdateState()
    {
        Invoke("da", 0.1f);
    }
    private void da()
    {
        float ratio = ((Time.time - deathTime) / timeToDecision) / 2;
        completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
        completionCircle.fillAmount = 1 - ratio;
        if (ratio > 1)
        {
            completionCircle.gameObject.SetActive(false);
        }
    }
    public void TryResumeGame()
    {
        AudioManager.Instance.PlaySFX(GameManager.Instance.clickSound);
        AdManager.Instance.ShowRewardedAd();
    }
    public void ResumeGame()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.motor.RespawnPlayer();
    }

    public void ToMenu()
    {
        GameManager.Instance.motor.ResetPlayer();
        AudioManager.Instance.PlaySFX(GameManager.Instance.clickSound);
        ///brain.ChangeState(GetComponent<GameStateInit>());
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        //GameManager.Instance.worldGeneration.ResetWorld();
        //GameManager.Instance.sceneChunkGeneration.ResetWorld();
        //GameManager.Instance.motor.anim.SetTrigger("Idle");
        
    }

    public void EnableRevive()
    { 
        completionCircle.gameObject.SetActive(true);
    }

    public void OnUnityAdsReady(string placementId)
    {
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log(message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //if (motor.totalDeaths == 3)
        // {
            //completionCircle.gameObject.SetActive(false);
        //    motor.totalDeaths = 0;
        //}
        switch(showResult)
        {
            case ShowResult.Failed:
                ToMenu();
                break;
            case ShowResult.Finished:
                ResumeGame();
                break;
            default:   
                break;
        }
    }
}
