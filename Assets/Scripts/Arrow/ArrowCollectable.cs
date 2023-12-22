using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ArrowCollectable : OnBallTouchEffectsPlayer
{
    [SerializeField] private Arrow.ArrowType _arrowType;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerDeck>().AddArrow(new(_arrowType));
            transform.GetChild(0).gameObject.SetActive(false);
            base.OnTriggerEnter2D(collision);
        }
    }
}
