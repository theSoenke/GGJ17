using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform _transform;

    private InputDirection _input
    {
        get
        {
            return new InputDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {

    }

    private void Update()
    {
    }

    
}
