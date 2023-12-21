using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class OnBallTouchEffectsPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Ball _))
            Instantiate(_effect, collision.contacts[0].point, Quaternion.identity);
    }
}
