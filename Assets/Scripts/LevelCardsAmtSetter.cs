using UnityEngine;

public class LevelCardsAmtSetter : MonoBehaviour
{
    [SerializeField] private int _cardsAmt = 3;
    public int CardAmt => _cardsAmt;
}
