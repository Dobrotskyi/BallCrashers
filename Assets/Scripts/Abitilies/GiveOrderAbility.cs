using System.Collections;
using UnityEngine;

public class GiveOrderAbility : Ability
{
    public override string Description => "Changes next turn of an enemy";

    public override string Name => "Order";

    protected override Abilities _abilityType => Abilities.Order;

    private PlayerDeck _playerDeck;
    private EnemyDeck _selectedDeck;
    private Arrow _changedArrow;
    private int _selectedCardIndex = -1;

    public void SetSelectedCardIndex(int index)
    {
        _selectedCardIndex = index;
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

            if (_selectedCardIndex != -1 && _changedArrow != null && _selectedDeck != null)
            {
                _selectedDeck.ArrowDeck[_selectedCardIndex] = _changedArrow;
                _selectedCardIndex = -1;
                _selectedDeck = null;
                _changedArrow = null;
                InvokeFinished();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _playerDeck = FindObjectOfType<PlayerDeck>(true);
    }
}
