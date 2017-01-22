using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text pressureText;
    public Text scoreText;


    private PlayerController player;


    private void Start()
    {
        player = GameController.Instance.Player;
    }

    private void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = GameController.Instance.Score.ToString();
    }
}
