using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour {

    [SerializeField]
    private GameObject _gameRoot;
	
	void Update ()
    {
		if(Input.anyKey)
        {
            _gameRoot.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
