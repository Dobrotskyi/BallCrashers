using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class OnBallTouchEffectsPlayer : MonoBehaviour
{
    protected AudioSource _as;
    [SerializeField] protected bool _destroyAfterTrigger;
    [SerializeField] private ParticleSystem _effect;

    protected virtual void Awake()
    {
        _as = GetComponent<AudioSource>();
        if (_as != null)
            _as.volume = SoundSettings.EFFECTS_VOLUME;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Ball _))
        {
            Instantiate(_effect, transform.position, Quaternion.identity);
            if (_destroyAfterTrigger)
                DestroySelf();
        }
    }

    protected virtual void DestroySelf()
    {
        GetComponent<Collider2D>().enabled = false;
        if (!SoundSettings.AudioMuted && _as)
        {
            _as.Play();
            Destroy(gameObject, _as.clip.length);
        }
        else
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Ball _))
            Instantiate(_effect, collision.contacts[0].point, Quaternion.identity);
    }
}
