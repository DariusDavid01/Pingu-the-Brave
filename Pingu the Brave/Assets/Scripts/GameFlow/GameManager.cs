using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;

public enum GameCamera
{
    init = 0,
    game = 1,
    shop = 2,
    respawn = 3
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    public PlayerMotor motor;
    public WorldGeneration worldGeneration;
    public SceneChunkGeneration sceneChunkGeneration;
    public GameObject[] cameras;
    public AudioClip clickSound;

    public GameState state;
    public bool isConnectedToGooglePlayServices;
    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        PlayerPrefs.SetInt("TutorialHasPlayed", 0);
        //DontDestroyOnLoad(this);
    }
    public void SignInToGooglePlayServices()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
        switch (result)
        {
            case SignInStatus.Success:
                isConnectedToGooglePlayServices = true;
                break;
            default:
                isConnectedToGooglePlayServices = false;
                break;
            }
        });
    }

    private void Start()
    {
        instance = this;
        state = GetComponent<GameStateInit>();
        state.Construct();
        SignInToGooglePlayServices();
    }

    private void Update()
    {
        state.UpdateState();
    }

    public void ChangeState(GameState s)
    {
        state.Destruct();
        state = s;
        state.Construct();
    }

    public void ChangeCamera(GameCamera c)
    {
        foreach (GameObject go in cameras)
            go.SetActive(false);

        cameras[(int)c].SetActive(true);
    }

    public void ChangeToSummer()
    {
        //Nullify();
        Initiate.Fade("GameSummer", Color.black, 2f);
        //SceneManager.UnloadSceneAsync(sceneName: "Game");
        //SceneManager.LoadScene(sceneName: "GameSummer",LoadSceneMode.Single);
    }
    
    public void ChangeToWinter()
    {
        //Nullify()
        // SceneManager.UnloadSceneAsync(sceneName: "GameSummer");
        // SceneManager.LoadScene(sceneName: "Game", LoadSceneMode.Single);
        Initiate.Fade("Game", Color.black, 2f);
    }
}
