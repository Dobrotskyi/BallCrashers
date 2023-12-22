using UnityEngine;

public class ArrowCollectable : MonoBehaviour
{
    [SerializeField] private Arrow.ArrowType _arrowType;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerDeck>().AddArrow(new(_arrowType));
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
