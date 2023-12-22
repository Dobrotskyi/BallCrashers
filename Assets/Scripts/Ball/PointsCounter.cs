using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    public const int FOR_BALL_HIT = 1;
    public const int FOR_WALL_HIT = 2;
    public const int FOR_FINISH = 3;

    public int Points
    {
        private set
        {
            _points = value;
            _amtField.text = _points.ToString();
        }
        get
        {
            return _points;
        }
    }
    private int _points = 0;

    [SerializeField] private TextMeshProUGUI _amtField;
    private Vector2 _velocityBeforeHit;
    private Collider2D _triggerCollider;
    private Rigidbody2D _rb;
    private bool _finished;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Ball _))
            _velocityBeforeHit = _rb.velocity.normalized;
        else
        {
            if (collision.TryGetComponent(out EndLevelPortal _) && !_finished)
            {
                Points += FOR_FINISH;
                Debug.Log($"{name} +{FOR_FINISH}");
                _finished = true;
            }
            if (collision.gameObject.CompareTag("Bonus"))
            {
                Debug.Log($"{name}, +{FOR_WALL_HIT}");
                Points += FOR_WALL_HIT;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out Ball _))
        {
            if (_velocityBeforeHit != Vector2.zero)
            {
                float angle = Vector2.SignedAngle(_velocityBeforeHit, (collision.contacts[0].point - (Vector2)transform.position).normalized);
                if (Mathf.Abs(angle) < 90)
                {
                    Debug.Log($"{name} + point");
                    Points += FOR_BALL_HIT;
                }
                _velocityBeforeHit = Vector2.zero;
            }
        }
    }

    private void Awake()
    {
        _triggerCollider = GetComponents<Collider2D>().Where(c => c.isTrigger).First();
        _rb = GetComponent<Rigidbody2D>();
        _velocityBeforeHit = Vector2.zero;
    }

    private void Update()
    {
        _triggerCollider.offset = _rb.velocity.normalized / 2;
    }
}
