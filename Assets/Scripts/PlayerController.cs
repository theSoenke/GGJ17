using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region fields & properties
    private Rigidbody2D _rigidbody;


    private InputDirection _input
    {
        get
        {
            return new InputDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
    #endregion

    #region unity messages
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody.gravityScale = 0;
    }

    private void Update()
    {
        MovePlayer(_input);
    }
    #endregion

    private void MovePlayer(InputDirection direction)
    {
        _rigidbody.AddForce(CalculateDirectionVector(direction));
    }

    private Vector2 CalculateDirectionVector(InputDirection direction)
    {
        var directionVector = new Vector2(direction.X,direction.Y).normalized;

        return directionVector;
    }
}
