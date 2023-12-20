using UnityEngine;

public class ArrowCard : MonoBehaviour
{
    private Rigidbody2D _playerRB;
    private Arrow _arrow;

    public void SetArrow(Arrow arrow) => _arrow = arrow;

    public void ShootPlayer()
    {
        _arrow.Launch(_playerRB);
    }

    private void Awake()
    {
        _playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }
}
