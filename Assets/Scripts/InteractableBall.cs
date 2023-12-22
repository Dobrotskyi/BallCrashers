using UnityEditor.Playables;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InteractableBall : MonoBehaviour
{
    private Animator _animator;
    private GiveOrderAbility _giveOrderAbility;

    public void Select()
    {
        _animator.SetBool("Selected", true);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _giveOrderAbility = FindObjectOfType<GiveOrderAbility>(true);
        _giveOrderAbility.Started += StartInteraction;
        _giveOrderAbility.Finished += StopInteraction;
    }

    private void OnDisable()
    {
        _giveOrderAbility.Started -= StartInteraction;
        _giveOrderAbility.Finished -= StopInteraction;
    }

    private void StartInteraction()
    {
        _animator.SetBool("Interacting", true);
    }

    private void StopInteraction()
    {
        _animator.SetBool("Interacting", false);
    }
}
