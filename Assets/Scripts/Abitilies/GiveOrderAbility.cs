using System.Collections;
using UnityEngine;

public class GiveOrderAbility : Ability
{
    public override string Description => "Gives the ability to change one card of an enemy";

    public override string Name => "Order";

    protected override Abilities _abilityType => Abilities.Order;

    private PlayerDeck _playerDeck;
    private EnemyDeck _selectedDeck;
    private Arrow _changedArrow;
    private int _selectedCardIndex = -1;
    private Arrow _selectedArrow;

    public void SetSelectedCardIndex(int index)
    {
        _selectedCardIndex = index;
        _playerDeck.ShowBasicCards();
    }

    public void SetSelectedCard(Arrow arrow)
    {
        _selectedArrow = arrow;
        _playerDeck.ShowBasicCards();
    }

    public void SetChangedArrow(Arrow.ArrowType arrowType)
    {
        _changedArrow = new(arrowType);
    }

    protected override IEnumerator Use()
    {
        InvokeStarted();
        InteractableBall enemyBall;
        while (AbilityInUse)
        {
            GameObject behindFinger = TouchInputs.GetObjectBehindFinger();
            if (TouchInputs.TouchBegan() && behindFinger)
                if (behindFinger.TryGetComponent(out enemyBall))
                {
                    enemyBall.Select();
                    _selectedDeck = enemyBall.GetComponent<EnemyDeck>();
                    _playerDeck.DisplayOtherDeck(_selectedDeck);
                }

            if (_selectedArrow != null && _changedArrow != null && _selectedDeck != null)
            {
                _selectedDeck.ArrowDeck[_selectedDeck.ArrowDeck.IndexOf(_selectedArrow)] = _changedArrow;

                _selectedCardIndex = -1;
                _selectedArrow = null;
                _selectedDeck = null;
                _changedArrow = null;
                InvokeFinished();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        EnemyDeck enemyDeck = FindObjectOfType<EnemyDeck>();
        if (enemyDeck && enemyDeck.GetUnused().Count <= 1)
            _button.interactable = false;
    }

    protected override void Awake()
    {
        base.Awake();

        _playerDeck = FindObjectOfType<PlayerDeck>(true);
    }
}
