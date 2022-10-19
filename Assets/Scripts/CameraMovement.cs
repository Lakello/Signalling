using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    private void Update()
    {
        transform.position = _camera.position;
    }
}
