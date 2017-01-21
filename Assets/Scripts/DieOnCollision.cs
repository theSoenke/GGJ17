using UnityEngine;

public class DieOnCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider others)
    {
        if (others.CompareTag("Rocks"))
        {
            Debug.Log("collision!");
            GameController.Instance.GameOver(GameResult.Lose);
        }
    }
}
