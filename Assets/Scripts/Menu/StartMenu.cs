using UnityEngine;

public class StartMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKey)
        {
            GameController.Instance.StartGame();
            gameObject.SetActive(false);
        }
    }
}
