using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    private void Update()
    {
        transform.position = _camera.position;
    }
}
