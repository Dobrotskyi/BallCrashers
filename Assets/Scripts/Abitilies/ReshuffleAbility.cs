using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReshuffleAbility : Ability
{
    public override string Description => "Gives the ability to change movement cards\n *Only before first turn*";

    public override string Name => "Reshuffle";

    protected override Abilities _abilityType => Abilities.Reshuffle;

    protected override IEnumerator Use()
    {
        InvokeStarted();
        yield return new WaitForEndOfFrame();
        FindObjectOfType<PlayerDeck>().Reshuffle();
        yield return new WaitForEndOfFrame();
        InvokeFinished();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        PlayerDeck deck = FindObjectOfType<PlayerDeck>();
        if (deck != null && deck.DeckUsed)
            _button.interactable = false;
    }
}
