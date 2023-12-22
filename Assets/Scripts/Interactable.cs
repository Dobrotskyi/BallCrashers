using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private ParticleSystem _onDestroyEffect;
    [SerializeField] private AudioClip _destroyedAudioClip;
    private Animator _animator;
    private bool _triggerParam;
    private AbilityHammer _hammer;

    public virtual void DestroySelf()
    {
        Instantiate(_onDestroyEffect, transform.position, Quaternion.identity);
        if (!SoundSettings.AudioMuted)
        {
            GameObject soundPlayer = new();
            var audioSource = soundPlayer.AddComponent<AudioSource>();
            audioSource.volume = SoundSettings.EFFECTS_VOLUME;
            audioSource.clip = _destroyedAudioClip;
            audioSource.playOnAwake = false;
            audioSource.Play();
            Destroy(audioSource, audioSource.clip.length);
        }
        Destroy(gameObject);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        _hammer = FindObjectOfType<AbilityHammer>(true);
        if (_hammer != null)
        {
            _hammer.Started += StartInteractable;
            _hammer.Finished += StopInteractable;
        }
    }

    protected virtual void OnDisable()
    {
        if (_hammer != null)
        {
            _hammer.Started -= StartInteractable;
            _hammer.Finished -= StopInteractable;
        }
    }

    private void StartInteractable()
    {
        if (GetComponent<Collider2D>().isTrigger)
        {
            _triggerParam = true;
            GetComponent<Collider2D>().isTrigger = false;
        }

        _animator.SetBool("Interacting", true);
    }
    private void StopInteractable()
    {
        if (!GetComponent<Collider2D>().isTrigger)
            GetComponent<Collider2D>().isTrigger = _triggerParam;

        _animator.SetBool("Interacting", false);
    }
}