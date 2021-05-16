using System.Collections;
using TMPro;
using UnityEngine;

public class GameStateInit : GameState
{
    public GameObject menuUI;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private TextMeshProUGUI fishcountText;
    [SerializeField] private AudioClip menuLoopMusic;
    public GameObject summerEdition;

    public override void Construct()
    {
            PlayerMotor motor = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerMotor>();
        motor.anim.ResetTrigger("Jump");
        motor.anim.ResetTrigger("Fall");
        motor.anim.ResetTrigger("Slide");
        motor.anim.ResetTrigger("Running");
        motor.anim.SetTrigger("Idle");

        GameManager.Instance.ChangeCamera(GameCamera.init);
        hiscoreText.text = "Cel mai mare scor: " + SaveManager.Instance.save.Highscore.ToString();
        fishcountText.text = "Pestisori : " + SaveManager.Instance.save.Fish.ToString();
        menuUI.SetActive(true);
        AudioManager.Instance.PlayMusicWithFade(menuLoopMusic);
        AudioManager.Instance.SetMusicVolume(0.1f);
        AudioManager.Instance.SetSFXVolume(0.1f);
        if (PlayerPrefs.GetInt("TutorialHasPlayed", 0) <= 0)
        {
            summerEdition.SetActive(true);
            PlayerPrefs.SetInt ("TutorialHasPlayed", 1); }
        StartCoroutine(destroySummer());
    }
   private IEnumerator destroySummer()
    {
        yield return new WaitForSeconds(6f);
        Destroy(summerEdition);
    }

    public override void Destruct()
    {
        menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {
        AudioManager.Instance.PlaySFX(GameManager.Instance.clickSound);
        brain.ChangeState(GetComponent<GameStateGame>());
        GameStats.Instance.ResetSession();
        GetComponent<GameStateDeath>().EnableRevive();
    }

    public void OnShopClick()
    {
        AudioManager.Instance.PlaySFX(GameManager.Instance.clickSound);
        brain.ChangeState(GetComponent<GameStateShop>());
    }

    public void OnAchievementClick()
    {
        AudioManager.Instance.PlaySFX(GameManager.Instance.clickSound);
        if (GameManager.Instance.isConnectedToGooglePlayServices)
        {
            Debug.Log("Achievement clicked");
            Social.ShowAchievementsUI();
        }   
        else
        {
            GameManager.Instance.SignInToGooglePlayServices();
        }
    }

    public void OnLeaderboardClick()
    {
        AudioManager.Instance.PlaySFX(GameManager.Instance.clickSound);
        if (GameManager.Instance.isConnectedToGooglePlayServices)
        {
            Debug.Log("Leaderboard clicked");
            Social.ShowLeaderboardUI();
        }
        else
        {
            GameManager.Instance.SignInToGooglePlayServices();
        }
    }
}
