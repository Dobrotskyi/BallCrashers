using System;
using UnityEngine;

public class PlayerArrowCard : MonoBehaviour
{
    public static event Action PlayerMadeTurn;

    private Rigidbody2D _playerRB;
    private Arrow _arrow;

    public void SetArrow(Arrow arrow) => _arrow = arrow;

    public void ShootPlayer()
    {
        _arrow.Launch(_playerRB);
        PlayerMadeTurn?.Invoke();
    }

    private void Awake()
    {
        _playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }
}
