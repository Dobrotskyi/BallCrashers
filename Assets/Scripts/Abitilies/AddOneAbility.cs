using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOneAbility : Ability
{
    public override string Description => "Gives the player one more card";

    public override string Name => "Add One";

    protected override Abilities _abilityType => Abilities.AddOne;

    protected override IEnumerator Use()
    {
        InvokeStarted();
        yield return new WaitForEndOfFrame();
        FindObjectOfType<PlayerDeck>().AddOneRandom();
        yield return new WaitForEndOfFrame();
        InvokeFinished();
    }
}
