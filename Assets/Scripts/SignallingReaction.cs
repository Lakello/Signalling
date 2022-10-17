using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignallingReaction : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] [Range(0.01f, 2f)] private float _multiplierDelta;
    [SerializeField] private UnityEvent _onPlayAudio;
    [SerializeField] private UnityEvent _onStopAudio;

    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private bool _isInvasion;
    private bool _isSuccess;

    public void OnInvasion(bool isInvasion)
    {
        _isInvasion = isInvasion;
        _isSuccess = false;

        if (_isInvasion)
        {
            _onPlayAudio?.Invoke();
        }
    }

    private void Update()
    {
        if (_isInvasion)
        {
            ChangeVolumeSmoothly(_audioSource.volume, _maxVolume);
        }
        else
        {
            ChangeVolumeSmoothly(_audioSource.volume, _minVolume);
        }
    }

    private void ChangeVolumeSmoothly(float current, float target)
    {
        if (_isSuccess == true) return;

        _audioSource.volume = Mathf.MoveTowards(current, target, _multiplierDelta * Time.deltaTime);

        if (current == target)
        {
            _isSuccess = true;

            if (_isInvasion == false) _onStopAudio?.Invoke();
        }
    }
}
