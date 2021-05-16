using TMPro;
using UnityEngine;

public class GameStateGame : GameState
{
    public static GameStateGame Instance { get { return instance; } }
    private static GameStateGame instance;
    public GameObject gameUI;
    [SerializeField] private TextMeshProUGUI fishCount; 
    [SerializeField] private TextMeshProUGUI scoreCount;
    [SerializeField] private AudioClip gameLoopMusic;
    public override void Construct()
    {
        base.Construct();
        GameManager.Instance.motor.ResumePlayer();
        GameManager.Instance.ChangeCamera(GameCamera.game);

        GameStats.Instance.onCollectFish += OnCollectFish;
        GameStats.Instance.onScoreChange += OnScoreChange;

        gameUI.transform.GetChild(2).gameObject.SetActive(false);
        gameUI.SetActive(true);
        AudioManager.Instance.PlayMusicWithFade(gameLoopMusic);
        Collider[] colliders = GameObject.Find("WorldGeneration").GetComponentsInChildren<Collider>();
        foreach (Collider childCollider in colliders)
        {
            childCollider.enabled = true;
        }
    }

    private void OnCollectFish(int amnCollected)
    {
        fishCount.text = GameStats.Instance.FishToText();
    }
    private void OnScoreChange(float score)
    {

        scoreCount.text = GameStats.Instance.ScoreToText();
    }

    public override void Destruct()
    {
        gameUI.SetActive(false);
        GameStats.Instance.onCollectFish -= OnCollectFish;
        GameStats.Instance.onScoreChange -= OnScoreChange;
        //PlayerMotor motor = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerMotor>();
        //motor.anim.SetTrigger("Idle");
    }

    public override void UpdateState()
    {
        GameManager.Instance.worldGeneration.ScanPosition();
        GameManager.Instance.sceneChunkGeneration.ScanPosition();
    }
}
