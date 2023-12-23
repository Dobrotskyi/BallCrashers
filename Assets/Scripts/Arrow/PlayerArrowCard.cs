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
        if (transform.parent.GetChild(index - 1).GetComponent<PlayerArrowCard>()._arrow.Used)
            return;

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
        PlayerDeck.TurnEnded += OnTurn;
        FindObjectOfType<RearrangeAbility>(true).Started += OnEnable;
    }

    private void OnDestroy()
    {
        PlayerDeck.TurnEnded -= OnTurn;
        if (FindObjectOfType<RearrangeAbility>(true) != null)
            FindObjectOfType<RearrangeAbility>(true).Started -= OnEnable;
    }

    private void OnDisable()
    {
        GetComponentsInChildren<Button>().ToList().ForEach(button => { button.interactable = true; });
        _animator.SetBool("Blocked", false);
    }

    private void OnEnable()
    {
        _animator.SetBool("Blocked", false);

        if (_arrow != null)
        {
            if (_arrow.Used)
            {
                GetComponent<Button>().interactable = false;
                GetComponentsInChildren<Button>().ToList().ForEach(button => { button.interactable = false; });
            }
            else
            {
                GetComponent<Button>().interactable = true;
                GetComponent<Button>().enabled = true;
                GetComponentsInChildren<Button>().ToList().ForEach(button => { button.interactable = true; });
            }
        }

        if (FindObjectOfType<PlayerDeck>().TurnStarted)
        {
            GetComponentsInChildren<Button>().ToList().ForEach(button => { button.interactable = false; });
            if (_arrow != null)
            {
                if (!_arrow.Used)
                {
                    GetComponent<Button>().interactable = true;
                    _animator.SetBool("Blocked", true);
                }
            }
            else
                _animator.SetBool("Blocked", true);

        }
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
