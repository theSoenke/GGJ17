using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            return _instance;
        }

        set
        {
            _instance = value;
        }
    }

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver(GameResult result)
    {
        switch(result)
        {
            case GameResult.Win:
                Win();
                break;
            case GameResult.Lose:
                Lose();
                break;
        }
    }

    private void Win()
    {
        //TODO: handle win state
    }

    private void Lose()
    {
        //TODO: handle lose state
        _player.SetActive(false);
    }
}
