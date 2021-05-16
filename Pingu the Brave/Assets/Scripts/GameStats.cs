using System;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    private static GameStats instance;
    public static GameStats Instance { get { return instance; } }

    //score
    public float score;
    public float highscore;
    public float distanceModifier = 1.5f;

    //fish
    public int totalFish;
    public int fishCollectedThisSession;
    public float pointsPerFish = 10.0f;
    public AudioClip fishCollectSFX;

    //internal cooldown
    private float lastScoreUpdate;
    private float scoreUpdateDelta = 0.2f;

    //action
    public Action<int> onCollectFish;
    public Action<float> onScoreChange;

    private void Awake()
    {
        instance = this;
        onCollectFish += SendAchivementProgress;
    }

    public void Update()
    {
        float s = GameManager.Instance.motor.transform.position.z * distanceModifier;
        s += fishCollectedThisSession * pointsPerFish;
        if (s > score)
        {
            score = s;
            if (Time.time - lastScoreUpdate > scoreUpdateDelta)
            {
                lastScoreUpdate = Time.time;
                onScoreChange?.Invoke(score);
            }
        }
    }

    private void SendAchivementProgress(int fishCount)
    {
        switch (fishCount)
        {
            case 25:
                Social.ReportProgress(GPGSIds.achievement_colecteaza_25_de_pestisori, 25.0f, null);
                break;
            case 50:
                Social.ReportProgress(GPGSIds.achievement_colecteaza_50_de_pestisori, 50.0f, null);
                break;
            case 100:
                Social.ReportProgress(GPGSIds.achievement_colecteaza_100_de_pestisori, 75.0f, null);
                break;
            case 200:
                Social.ReportProgress(GPGSIds.achievement_colecteaza_200_de_pestisori, 100.0f, null);
                break;
            default:
                break;
        }
    }

    public void CollectFish()
    {
        fishCollectedThisSession++;
        onCollectFish?.Invoke(fishCollectedThisSession);
        AudioManager.Instance.PlaySFX(fishCollectSFX,0.7f);
    }

    public void ResetSession()
    {
        score = 0;
        fishCollectedThisSession = 0;
        onCollectFish?.Invoke(fishCollectedThisSession);
        onScoreChange?.Invoke(score);
    }

    public string ScoreToText()
    {
        return score.ToString("0000000");
    }

    public string FishToText()
    {
        return fishCollectedThisSession.ToString("000");
    }
}
