using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    public const int FOR_BALL_HIT = 1;
    public const int FOR_WALL_HIT = 2;

    private Vector2 _velocity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _velocity = transform.GetComponent<Rigidbody2D>().velocity.normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Ball _))
        {
            if (Mathf.Abs(_velocity.y) > Mathf.Abs(_velocity.x))
            {
                if (_velocity.y > 0)
                {
                    if (collision.contacts[0].point.y > transform.position.y)
                        Debug.Log($"{name}1, +point");
                }
                else if (_velocity.y < 0)
                    if (collision.contacts[0].point.y < transform.position.y)
                        Debug.Log($"{name}2, +point");
            }
            else
            {
                if (_velocity.x > 0)
                {
                    if (collision.contacts[0].point.x > transform.position.x)
                        Debug.Log($"{name}3, +point");
                }
                else if (_velocity.x < 0)
                    if (collision.contacts[0].point.x < transform.position.x)
                        Debug.Log($"{name}4, +point");
            }
        }

        if (collision.gameObject.CompareTag("Bonus"))
            Debug.Log($"{name}, +{FOR_WALL_HIT}");
    }
}
