using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class OnBallTouchEffectsPlayer : MonoBehaviour
{
    protected AudioSource _as;
    [SerializeField] protected bool _destroyAfterTrigger;
    [SerializeField] private bool _ignoreOnTriggerEnter;
    [SerializeField] protected ParticleSystem _effect;

    protected virtual void Awake()
    {
        _as = GetComponent<AudioSource>();
        if (_as != null)
            _as.volume = SoundSettings.EFFECTS_VOLUME;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (_ignoreOnTriggerEnter) return;

        if (collision.transform.TryGetComponent(out Ball _))
        {
            InstantiateEffect(transform.position);
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
            InstantiateEffect(collision.contacts[0].point);
    }

    protected void InstantiateEffect(Vector2 point)
    {
        ParticleSystem effect = Instantiate(_effect, point, Quaternion.identity);
        if (effect.TryGetComponent(out AudioSource source))
        {
            source.volume = SoundSettings.EFFECTS_VOLUME;
            if (!SoundSettings.AudioMuted)
                source.enabled = true;
        }
    }
}
