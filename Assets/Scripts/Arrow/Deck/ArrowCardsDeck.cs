using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ArrowCardsDeck : MonoBehaviour
{
    protected List<Arrow> _arrowCards = new();

    public List<Arrow> GetUnused() => _arrowCards.Where(a => !a.Used).ToList();

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
