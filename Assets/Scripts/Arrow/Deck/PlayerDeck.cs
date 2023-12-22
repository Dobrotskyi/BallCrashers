using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeck : ArrowCardsDeck
{
    [Serializable]
    public struct PrefabArrowTypePair
    {
        public GameObject Prefab;
        public Arrow.ArrowType ArrowType;
    }

    [SerializeField] private List<PrefabArrowTypePair> _prefabsList;
    [SerializeField] private HorizontalLayoutGroup _deckGroup;

    public bool DeckUsed => _arrowCards.Where(a => a.Used).Count() > 0;

    public void AddOne()
    {
        Arrow newArrow = new();
        _arrowCards.Add(newArrow);
        CreateCard(newArrow);
    }

    public void Reshuffle()
    {
        int amt = _arrowCards.Count;
        foreach (Transform child in _deckGroup.transform)
            Destroy(child.gameObject);
        _arrowCards.Clear();

        for (int i = 0; i < amt; i++)
            _arrowCards.Add(new Arrow());
        AwakeAdditionalSetup();
    }

    protected override void AwakeAdditionalSetup()
    {
        foreach (var arrow in _arrowCards)
            CreateCard(arrow);
    }

    private void CreateCard(Arrow arrow)
    {
        GameObject button = Instantiate(_prefabsList.Where(p => p.ArrowType == arrow.Type).First().Prefab, _deckGroup.transform);
        button.GetComponent<PlayerArrowCard>().SetArrow(arrow);
    }
}

