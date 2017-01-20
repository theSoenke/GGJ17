using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnCollision : MonoBehaviour
{

	private void OnTriggerEnter(Collider others)
    {
        Debug.Log("collision!");
        GameController.Instance.GameOver(GameResult.Lose);
    }
}
