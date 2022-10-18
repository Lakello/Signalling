using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

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
    private Coroutine _changeVolumeSmoothlyCoroutine;

    private IEnumerator ChangeVolumeSmoothly(float target)
    {
        while (_isSuccess == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, _multiplierDelta * Time.deltaTime);

            if (_audioSource.volume == target)
            {
                _isSuccess = true;

                if (_isInvasion == false) _onStopAudio?.Invoke();
            }

            yield return null;
        }
    }

    public void OnInvasion(bool isInvasion)
    {
        _isInvasion = isInvasion;
        _isSuccess = false;

        if (_isInvasion)
        {
            _onPlayAudio?.Invoke();
        }

        if (_changeVolumeSmoothlyCoroutine != null)
        {
            StopCoroutine(_changeVolumeSmoothlyCoroutine);
        }

        if (_isInvasion)
        {
            _changeVolumeSmoothlyCoroutine = StartCoroutine(ChangeVolumeSmoothly(_maxVolume));
        }
        else
        {
            _changeVolumeSmoothlyCoroutine = StartCoroutine(ChangeVolumeSmoothly(_minVolume));
        }
    }
}
