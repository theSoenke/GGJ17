using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {

    [SerializeField]
    private GameObject _game;
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.anyKey)
        {
            _game.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
