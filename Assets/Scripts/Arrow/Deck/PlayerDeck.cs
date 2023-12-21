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

    protected override void AwakeAdditionalSetup()
    {
        foreach (var arrow in _arrowCards)
        {
            GameObject button = Instantiate(_prefabsList.Where(p => p.ArrowType == arrow.Type).First().Prefab, _deckGroup.transform);
            button.GetComponent<PlayerArrowCard>().SetArrow(arrow);
        }
    }
}

