using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    private void Start()
    {
        int score = GameController.Instance.Score;
        int highscore = GameController.HighScore;
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            GameController.Instance.RestartGame();
        }
    }
}
