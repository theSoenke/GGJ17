using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameRoot;
    [SerializeField]
    private GameObject hudRoot;

    private void Update()
    {
        if (Input.anyKey)
        {
            _gameRoot.SetActive(true);
            hudRoot.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
