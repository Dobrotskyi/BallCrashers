using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyDeck : ArrowCardsDeck
{
    private Rigidbody2D _rb;

    public List<Arrow> ArrowDeck => _arrowCards;

    protected override void AwakeAdditionalSetup()
    {
        PlayerDeck.TurnEnded += MakeTurn;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        PlayerDeck.TurnEnded -= MakeTurn;
    }

    private void MakeTurn()
    {
        var unusedArrows = UnusedArrows();
        if (unusedArrows.Count == 0)
            return;
        unusedArrows[0].Launch(_rb);
    }

    private List<Arrow> UnusedArrows()
    {
        return _arrowCards.Where(a => !a.Used).ToList();
    }
}
