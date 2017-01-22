using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static bool newGame = true;

    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private int pressureWarning = 80;
    [SerializeField]
    private int pressureCritical = 90;
    [SerializeField]
    private ScannerEffectDemo scannerEffect;
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
    private int score;
    private GameState gameState;
    private readonly List<Mine> mines = new List<Mine>();


    public PlayerController Player
    {
        get
        {
            return player;
        }
    }

    public int Score
    {
        get { return score; }
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

    private enum GameState
    {
        Pause,
        Running,
        Lost
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
        }
    }

    private void Start()
    {
        if (newGame)
        {
            ShowStartMenu();
        }
        else
        {
            StartGame();
        }
    }

    private void Update()
    {
        if (Pressure == PressureStatus.Boom)
        {
            //Implode();
        }

        if (gameState == GameState.Running)
        {
            score = (int)(Time.time - startTime);
        }
    }

    public void StartGame()
    {
        newGame = false;
        gameState = GameState.Running;
        startTime = Time.time;
        gameRoot.SetActive(true);
        hudMenu.SetActive(true);
        startMenu.SetActive(false);
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

    public void StartScan()
    {
        scannerEffect.RunScan();

        var audioSource = scannerEffect.GetComponent<AudioSource>();
        audioSource.Play();

        BlinkMines();
    }

    public void RegisterMine(Mine mine)
    {
        mines.Add(mine);
    }

    public void UnregisterMine(Mine mine)
    {
        mines.Remove(mine);
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
        hudMenu.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    private void BlinkMines()
    {
        foreach (var mine in mines)
        {
            mine.ShowMine();
        }
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
        gameState = GameState.Lost;
        SaveHighscore(score);
        player.gameObject.SetActive(false);
        ShowGameOverMenu();
    }
}
