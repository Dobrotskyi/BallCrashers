using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyDeck : ArrowCardsDeck
{
    private Rigidbody2D _rb;
    protected override void AwakeAdditionalSetup()
    {
        PlayerArrowCard.PlayerMadeTurn += MakeTurn;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        PlayerArrowCard.PlayerMadeTurn -= MakeTurn;
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
