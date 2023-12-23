using System.Collections;
using UnityEngine;

public class AbilityHammer : Ability, IInteractableAbility
{
    public override string Description => "Gives the ability to destroy obstacles \nor\n Adds a horizontal obstacle on that place";
    public override string Name => "Hammer";
    protected override Abilities _abilityType => Abilities.Hammer;

    [Header("Can leave empty if in shop")]
    [SerializeField] private GameObject _obstacle;

    protected override IEnumerator Use()
    {
        Debug.Log("InvokeStarted");
        InvokeStarted();
        Interactable selectedObject = null;
        while (AbilityInUse)
        {
            if (TouchInputs.TouchBegan())
                selectedObject = TouchInputs.GetObjectBehindFinger()?.GetComponent<Interactable>();

            if (selectedObject != null)
            {
                selectedObject.DestroySelf();
                InvokeFinished();
            }
            else
            {
                if (TouchInputs.TouchBegan() && !TouchInputs.OverUINotClickthrough())
                {
                    Instantiate(_obstacle).transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                    InvokeFinished();
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

}
