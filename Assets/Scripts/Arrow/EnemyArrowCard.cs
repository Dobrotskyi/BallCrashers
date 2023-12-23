using UnityEngine;
using UnityEngine.UI;

public class EnemyArrowCard : MonoBehaviour
{
    public Arrow Arrow;

    private Button _button;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        //FindObjectOfType<GiveOrderAbility>(true).SetSelectedCardIndex(_button.transform.GetSiblingIndex());
        FindObjectOfType<GiveOrderAbility>(true).SetSelectedCard(Arrow);
    }
}
