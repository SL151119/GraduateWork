using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip[] sounds;
    private AudioSource _audioSource;
    private int _soundIndex = 0;

    private bool _isCoroutineRunning = false;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(WaitBeforeStartMusic(1f));
    }

    private void Update()
    {
        if (!_isCoroutineRunning && !_audioSource.isPlaying)
        {
            StartCoroutine(WaitBeforeNextMusic(2f));
        }
    }

    private IEnumerator WaitBeforeStartMusic(float waitForSeconds)
    {
        _isCoroutineRunning = true;
        yield return new WaitForSeconds(waitForSeconds);
        _audioSource.clip = sounds[_soundIndex];
        _audioSource.Play();
        _isCoroutineRunning = false;
    }

    private IEnumerator WaitBeforeNextMusic(float waitForSeconds)
    {
        _isCoroutineRunning = true;
        _audioSource.Stop();
        yield return new WaitForSeconds(waitForSeconds);
        if (_soundIndex < sounds.Length - 1)
        {
            _soundIndex += 1;
        }
        else
        {
            _soundIndex = 0;
        }
        _audioSource.clip = sounds[_soundIndex];
        _audioSource.Play();
        _isCoroutineRunning = false;
    }
}
