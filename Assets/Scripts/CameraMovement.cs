using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed;

    private Transform _camera;

    private Vector3 _movementDirection = new Vector3(1, 0, 0);
    
    private void Awake()
    {
        _camera = GetComponent<Transform>();
    }

    private void Update()
    {
        _camera.position += _movementDirection * _movementSpeed * Time.deltaTime;
    }
}
