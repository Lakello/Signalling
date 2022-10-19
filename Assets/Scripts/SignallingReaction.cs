using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class SignallingReaction : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField][Range(0.01f, 2f)] private float _multiplierDelta;
    [SerializeField] private UnityEvent _onPlayAudio;
    [SerializeField] private UnityEvent _onStopAudio;

    private float _maxVolume = 1f;
    private float _minVolume = 0f;
    private bool _isInvasion;
    private Coroutine _changeVolumeSmoothlyCoroutine;

    public void OnInvasion(bool isInvasion)
    {
        _isInvasion = isInvasion;

        if (_isInvasion)
        {
            _onPlayAudio?.Invoke();
        }

        if (_changeVolumeSmoothlyCoroutine != null)
        {
            StopCoroutine(_changeVolumeSmoothlyCoroutine);
        }

        _changeVolumeSmoothlyCoroutine = StartCoroutine(ChangeVolumeSmoothly());
    }

    private IEnumerator ChangeVolumeSmoothly()
    {
        float target = _audioSource.volume == 0 ? _maxVolume : _minVolume;

        while (_audioSource.volume != target)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, target, _multiplierDelta * Time.deltaTime);

            yield return null;
        }

        if (_isInvasion == false)
        {
            _onStopAudio?.Invoke();
        }
    }
}