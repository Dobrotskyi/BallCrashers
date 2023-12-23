using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerArrowCard : MonoBehaviour
{
    private const float COOLDOWN = 2f;

    private Rigidbody2D _playerRB;
    private Arrow _arrow;
    private Animator _animator;
    private HorizontalLayoutGroup _deckGroup;

    public void SetArrow(Arrow arrow) => _arrow = arrow;

    public void ShootPlayer()
    {
        if (_arrow.Used)
            return;

        _arrow.Launch(_playerRB);
    }

    public void GoLeft()
    {
        int index = transform.GetSiblingIndex();
        if (index == 0) return;

        transform.SetSiblingIndex(index - 1);
    }

    public void GoRight()
    {
        int index = transform.GetSiblingIndex();
        if (index == transform.parent.childCount - 1) return;

        transform.SetSiblingIndex(index + 1);
    }

    private void Awake()
    {
        _playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _deckGroup = GetComponentInParent<HorizontalLayoutGroup>();
        if (FindObjectOfType<PlayerDeck>().DeckUsed)
        {
            GetComponentsInChildren<Button>().ToList().ForEach(button => { button.interactable = false; });
            _animator.SetBool("Blocked", true);
        }
        PlayerDeck.TurnEnded += OnTurn;
    }

    private void OnDestroy()
    {
        PlayerDeck.TurnEnded -= OnTurn;
    }

    private void OnTurn()
    {
        if (!_animator.GetBool("Blocked"))
            GetComponentsInChildren<Button>().ToList().ForEach(button => { button.interactable = false; });
        if (_arrow.Used)
        {
            _animator.SetBool("Blocked", false);
            GetComponent<Button>().interactable = false;
        }
        else
            _animator.SetBool("Blocked", true);
    }
}
