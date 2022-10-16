using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator _selfAnimator;
    [SerializeField] private string _triggerWalk = "OnWalk";
    [SerializeField] private string _triggerBackWalk = "OnBackWalk";
    [SerializeField] private string _triggerIdle = "OnIdle";
    [SerializeField] private string _triggerRun = "OnRun";
    [SerializeField] private string _triggerJump = "OnJump";
    [SerializeField] private KeyCode _keyRun = KeyCode.LeftShift;
    [SerializeField] private KeyCode _keyJump = KeyCode.Space;

    private Rigidbody _selfRigidbody;
    private float _verticalInput;

    private void Start()
    {
        _selfRigidbody = GetComponent<Rigidbody>();
        _selfRigidbody.freezeRotation = true;
    }

    private void Update()
    {
        InputFromDevice();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void InputFromDevice()
    {
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        if (_verticalInput > 0)
        {
            if (Input.GetKey(_keyRun))
            {
                _selfAnimator.SetTrigger(_triggerRun);
                _selfAnimator.ResetTrigger(_triggerWalk);
                _selfAnimator.ResetTrigger(_triggerIdle);
            }
            else
            {
                _selfAnimator.SetTrigger(_triggerWalk);
                _selfAnimator.ResetTrigger(_triggerRun);
                _selfAnimator.ResetTrigger(_triggerIdle);
            }
        }

        if (_verticalInput < 0)
        {
            _selfAnimator.SetTrigger(_triggerBackWalk);
            _selfAnimator.ResetTrigger(_triggerIdle);
        }

        if (_verticalInput == 0)
        {
            _selfAnimator.SetTrigger(_triggerIdle);
            _selfAnimator.ResetTrigger(_triggerBackWalk);
            _selfAnimator.ResetTrigger(_triggerWalk);
            _selfAnimator.ResetTrigger(_triggerJump);
        }

        if (Input.GetKey(_keyJump))
        {
            _selfAnimator.SetTrigger(_triggerJump);
            _selfAnimator.ResetTrigger(_triggerIdle);
        }
    }
}
