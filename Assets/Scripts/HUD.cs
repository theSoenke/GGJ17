using System;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text pressureText;



    private PlayerController player;


    private void Start()
    {
        player = GameController.Instance.Player;
    }

    private void Update()
    {
        UpdatePressure();
    }

    private void UpdatePressure()
    {
        GameController.PressureStatus pressureStatus = GameController.Instance.Pressure;

        switch (pressureStatus)
        {
            case GameController.PressureStatus.Normal:
                pressureText.color = Color.white;
                break;
            case GameController.PressureStatus.Warning:
                pressureText.color = Color.yellow;
                break;
            case GameController.PressureStatus.Critical:
                pressureText.color = Color.red;
                break;
            case GameController.PressureStatus.Boom:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        var pressure = (int)player.WaterPressure();
        pressureText.text = pressure.ToString();
    }
}
