using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Ball _))
        {
            Instantiate(_effect, collision.contacts[0].point, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
