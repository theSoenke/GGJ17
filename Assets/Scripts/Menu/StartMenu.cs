using UnityEngine;

public class StartMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            GameController.Instance.StartGame();
            gameObject.SetActive(false);
        }
    }
}
