using System.Collections;
using UnityEngine;

public class GiveOrderAbility : Ability
{
    public override string Description => "Changes next turn of an enemy";

    public override string Name => "Order";

    protected override Abilities _abilityType => Abilities.Order;

    private PlayerDeck _playerDeck;

    protected override IEnumerator Use()
    {
        InvokeStarted();
        InteractableBall enemyBall;
        while (AbilityInUse)
        {
            GameObject behindFinger = TouchInputs.GetObjectBehindFinger();
            if (behindFinger)
                if (behindFinger.TryGetComponent(out enemyBall))
                {
                    enemyBall.Select();
                    _playerDeck.DisplayOtherDeck(enemyBall.GetComponent<EnemyDeck>());
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
