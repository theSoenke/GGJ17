using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameRoot;
    [SerializeField]
    private GameObject _hudRoot;

    void Update()
    {
        if (Input.anyKey)
        {
            _gameRoot.SetActive(true);
            _hudRoot.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
