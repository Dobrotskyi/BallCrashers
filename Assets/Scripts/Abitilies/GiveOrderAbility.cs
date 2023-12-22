using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveOrderAbility : Ability
{
    public override string Description => "Changes next turn of an enemy";

    public override string Name => "Order";

    protected override Abilities _abilityType => Abilities.Order;

    protected override IEnumerator Use()
    {
        InvokeStarted();
        yield return new WaitForEndOfFrame();
        InvokeFinished();
    }
}
