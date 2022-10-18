using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float _sensitivity;
    [SerializeField] private Transform _player;
    [SerializeField] private float _xRotation;
    [SerializeField] private float _yRotation;

    private float limitRotation = 90f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * _sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * _sensitivity * Time.deltaTime;

        _yRotation += mouseX;
        _xRotation -= mouseY;

        _xRotation = Mathf.Clamp(_xRotation, -limitRotation, limitRotation);

        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        _player.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
