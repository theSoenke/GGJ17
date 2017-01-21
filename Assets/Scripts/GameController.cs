using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private int pressureWarning = 80;
    [SerializeField]
    private int pressureCritical = 90;
    [SerializeField]
    private GameObject gameRoot;
    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject gameOverMenu;
    [SerializeField]
    private GameObject hudMenu;


    public static GameController Instance { get; private set; }

    private float startTime;


    public PlayerController Player
    {
        get
        {
            return player;
        }
    }

    public int Score
    {
        get { return (int)(Time.time - startTime); }
    }

    public static int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt("highscore");
        }
    }

    public PressureStatus Pressure
    {
        get
        {
            var pressure = (int)player.WaterPressure();

            if (pressure < pressureWarning)
            {
                return PressureStatus.Normal;
            }
            if (pressure >= pressureCritical)
            {
                return PressureStatus.Critical;
            }
            if (pressure >= pressureWarning)
            {
                return PressureStatus.Warning;
            }
            return PressureStatus.Boom;
        }
    }

    public enum PressureStatus
    {
        Normal,
        Warning,
        Critical,
        Boom
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        ShowStartMenu();
    }

    private void Update()
    {
        if (Pressure == PressureStatus.Boom)
        {
            Implode();
        }

        int score = (int)(Time.time - startTime);
    }

    public void StartGame()
    {
        startTime = Time.time;
        hudMenu.SetActive(true);
        gameRoot.SetActive(true);
        hudMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void GameOver(GameResult result)
    {
        switch (result)
        {
            case GameResult.Win:
                Win();
                break;
            case GameResult.Lose:
                Lose();
                break;
        }
    }

    public void HitMine()
    {
        Lose();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }

    private void ShowStartMenu()
    {
        startMenu.SetActive(true);
    }

    private void ShowGameOverMenu()
    {
        gameRoot.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    private void Implode()
    {
        Lose();
    }

    private void SaveHighscore(int score)
    {
        int highscore = HighScore;

        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    private void Win()
    {
        //TODO: handle win state
    }

    private void Lose()
    {
        //TODO: handle lose state
        SaveHighscore(Score);
        player.gameObject.SetActive(false);
        ShowGameOverMenu();
    }
}
