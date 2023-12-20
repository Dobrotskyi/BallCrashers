using System.Collections.Generic;
using UnityEngine;

public class ArrowCardsDeck : MonoBehaviour
{
    public const int ARROWS_AMT = 3;
    protected List<Arrow> _arrowCards = new();

    private void Awake()
    {
        CreateArrowDeck();
        LogArrows();
    }
    private void CreateArrowDeck()
    {
        for (int i = 0; i < ARROWS_AMT; i++)
            _arrowCards.Add(new());
    }

    private void LogArrows()
    {
        foreach (var arrow in _arrowCards)
            Debug.Log(arrow.Type);
    }
}
