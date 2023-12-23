using System.Collections;
using UnityEngine;

public class RearrangeAbility : Ability
{
    public override string Description => "Gives the ability to change order of cards\n *Only after first turn*";

    public override string Name => "Rearrange";

    protected override Abilities _abilityType => Abilities.Rearrange;

    protected override IEnumerator Use()
    {
        FindObjectOfType<PlayerDeck>().AllowRearrange();

        yield return new WaitForEndOfFrame();
        InvokeStarted();
        yield return new WaitForEndOfFrame();
        InvokeFinished();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        PlayerDeck deck = FindObjectOfType<PlayerDeck>();
        if (deck == null)
            return;
        if (deck.GetUnused().Count <= 1 || !deck.TurnStarted)
            _button.interactable = false;
    }
}
