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

        float target;

        if (_isInvasion)
        {
            _onPlayAudio?.Invoke();
            target = _maxVolume;
        }
        else
        {
            target = _minVolume;
        }

        if (_changeVolumeSmoothlyCoroutine != null)
        {
            StopCoroutine(_changeVolumeSmoothlyCoroutine);
        }

        _changeVolumeSmoothlyCoroutine = StartCoroutine(ChangeVolumeSmoothly(target));
    }

    private IEnumerator ChangeVolumeSmoothly(float target)
    {
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
