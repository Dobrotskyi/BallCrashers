using UnityEngine;

public class Teleport : OnBallTouchEffectsPlayer
{
    private Vector2 _minMaxX = new Vector2(-1.84f, 1.84f);
    private Vector2 _minMaxY = new Vector2(-4.35f, 4.35f);

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.TryGetComponent(out Ball _))
        {
            while (true)
            {
                Vector2 endPoint = new Vector2(Random.Range(_minMaxX.x, _minMaxX.y), Random.Range(_minMaxY.x, _minMaxY.y));
                Debug.Log(endPoint);
                if (Physics2D.OverlapCircle(endPoint, collision.bounds.size.x / 2, ~LayerMask.GetMask("CamConfiner")) == null)
                {
                    collision.transform.position = endPoint;
                    Instantiate(_effect, endPoint, Quaternion.identity);
                    break;
                }
            }

            if (!SoundSettings.AudioMuted && _as)
            {
                _as.Play();
            }
        }
    }
}
