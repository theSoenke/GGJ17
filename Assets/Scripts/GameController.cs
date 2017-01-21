using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private int pressureWarning = 80;
    [SerializeField]
    private int pressureCritical = 90;

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

    private void Implode()
    {
        Lose();
    }

    private void SaveHighscore(int score)
    {
        int highscore = GetHighscore();

        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    private int GetHighscore()
    {
        return PlayerPrefs.GetInt("highscore");
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
    }
}
