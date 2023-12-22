using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EndLevelPortal : MonoBehaviour
{
    public static event Action LevelFinished;

    [SerializeField] private ParticleSystem _levelPassed;
    private AudioSource _as;
    private List<string> _finishedBalls = new();

    private void Start()
    {
        _as = GetComponent<AudioSource>();
        _as.volume = SoundSettings.EFFECTS_VOLUME;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ball _))
        {
            if (_finishedBalls.Contains(collision.gameObject.name))
                return;

            Instantiate(_levelPassed, transform.position, Quaternion.identity);
            LevelFinished?.Invoke();
            if (!SoundSettings.AudioMuted)
                _as.Play();
            _finishedBalls.Add(collision.gameObject.name);
        }
    }
}
