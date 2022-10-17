using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignallingZone : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> _onInvasion;

    private bool _isInvasion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && _isInvasion == false)
        {
            _isInvasion = true;
            _onInvasion?.Invoke(_isInvasion);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player) && _isInvasion == true)
        {
            _isInvasion = false;
            _onInvasion?.Invoke(_isInvasion);
        }
    }
}
