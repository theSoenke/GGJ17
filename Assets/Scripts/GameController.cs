using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static bool newGame = true;

    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private ScannerEffectDemo scannerEffect;
    [SerializeField]
    private float scannerCooldown = 2;
    [SerializeField]
    private CameraMovement cameraMovement;
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
    private float cooldownTimer;


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

    public float ScanCooldown
    {
        get
        {
            return cooldownTimer / scannerCooldown;
        }
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
        if (gameState == GameState.Running)
        {
            score = (int)(Time.time - startTime);
        }

        cooldownTimer += Time.deltaTime;
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

    public void StartScan()
    {
        if (cooldownTimer >= scannerCooldown)
        {
            scannerEffect.RunScan();

            var audioSource = scannerEffect.GetComponent<AudioSource>();
            audioSource.Play();

            BlinkMines();

            cooldownTimer = 0;
        }
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

    private void SaveHighscore(int score)
    {
        int highscore = HighScore;

        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void Lose()
    {
        gameState = GameState.Lost;
        player.gameObject.SetActive(false);
        //cameraMovement.enabled = false;
        SaveHighscore(score);
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(5);
        ShowGameOverMenu();
    }
}
