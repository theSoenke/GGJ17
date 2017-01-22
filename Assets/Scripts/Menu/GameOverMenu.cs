using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text scoreText;
    public Text highscoreText;


    private void Start()
    {
        int score = GameController.Instance.Score;
        int highscore = GameController.HighScore;

        scoreText.text = score.ToString();
        highscoreText.text = highscore.ToString();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            GameController.Instance.RestartGame();
        }
    }
}
