using System.Collections.Generic;
using UnityEngine;

public class ArrowCardsDeck : MonoBehaviour
{
    protected List<Arrow> _arrowCards = new();

    protected void Awake()
    {
        CreateArrowDeck();
        AwakeAdditionalSetup();
    }
    protected virtual void AwakeAdditionalSetup() { }

    private void CreateArrowDeck()
    {
        int amt = FindObjectOfType<LevelCardsAmtSetter>().CardAmt;
        for (int i = 0; i < amt; i++)
            _arrowCards.Add(new());
    }
}
