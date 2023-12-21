using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArrowCard : MonoBehaviour
{
    private const float COOLDOWN = 2f;

    public static event Action PlayerMadeTurn;

    private Rigidbody2D _playerRB;
    private Arrow _arrow;
    private bool _onCooldown;

    public void SetArrow(Arrow arrow) => _arrow = arrow;

    public void ShootPlayer()
    {
        if (_onCooldown)
            return;

        _arrow.Launch(_playerRB);
        PlayerMadeTurn?.Invoke();
    }

    private void Awake()
    {
        _playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        var button = GetComponent<Button>();
        PlayerMadeTurn += OnTurn;
    }

    private void OnDestroy()
    {
        PlayerMadeTurn -= OnTurn;
    }

    private void OnTurn()
    {
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForEndOfFrame();
        _onCooldown = true;
        var button = GetComponent<Button>();
        if (_arrow.Used)
            button.interactable = false;
        else
        {
            button.enabled = false;
            button.targetGraphic.color = new Color(0, 0, 0, 0.5f);
        }
        float time = Time.time;
        while (Time.time - time < COOLDOWN)
            yield return new WaitForEndOfFrame();
        if (!_arrow.Used)
        {
            button.enabled = true;
            button.targetGraphic.color = Color.white;
        }

        _onCooldown = false;
    }
}
