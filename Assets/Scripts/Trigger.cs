using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private AudioClip _signallingAudio;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private UnityEvent<AudioClip> _onSignallingPlay;
    [SerializeField] private UnityEvent _onSignallingStop;

    private bool _isEnabled;
    private bool _isExit;
    private float _maxVolume = 1f;
    private float _minVolume = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && _isEnabled == false)
        {
            _onSignallingPlay?.Invoke(_signallingAudio);
            _isEnabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _isExit = true;
        }
    }

    private void Update()
    {
        if (_isExit) StopSignalling();
    }

    private void StopSignalling()
    {
        if (_audioSource.volume == _minVolume) _isExit = false;

        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, Time.deltaTime);

        if (_isExit == false)
        {
            _onSignallingStop?.Invoke();
            _isEnabled = false;
        }
    }
}
