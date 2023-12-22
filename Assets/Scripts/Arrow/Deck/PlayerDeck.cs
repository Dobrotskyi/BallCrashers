using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeck : ArrowCardsDeck
{
    public static event Action TurnEnded;

    [Serializable]
    public struct PrefabArrowTypePair
    {
        public GameObject Prefab;
        public Arrow.ArrowType ArrowType;
    }

    [SerializeField] private List<PrefabArrowTypePair> _prefabsList;
    [SerializeField] private HorizontalLayoutGroup _deckGroup;
    private float _startHeight = 0;

    public bool DeckUsed => _arrowCards.Where(a => a.Used).Count() > 0;

    public void HideDeck()
    {
        StartCoroutine(ChangeHeight(0));
    }

    public void ShowDeck()
    {
        StartCoroutine(ChangeHeight(_startHeight));
    }

    private IEnumerator ChangeHeight(float endValue)
    {
        float duration = 0.7f;
        RectTransform rectTransform = GetComponent<RectTransform>();
        float startHeight = rectTransform.sizeDelta.y;

        float timeElapsed = 0;
        float t = timeElapsed;
        while (timeElapsed < duration)
        {

            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, Mathf.Lerp(startHeight, endValue, t));

            t = timeElapsed / duration;
            t = t * t * (3f - 2f * t);
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, endValue);
    }

    public void AddOneRandom()
    {
        Arrow newArrow = new();
        _arrowCards.Add(newArrow);
        CreateCard(newArrow);
    }

    public void AddArrow(Arrow arrow)
    {
        _arrowCards.Add(arrow);
        CreateCard(arrow);
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

    public void StartTurn()
    {
        StartCoroutine(MakeTurns());
    }

    private IEnumerator MakeTurns()
    {
        int childIndex = 0;
        while (childIndex < _deckGroup.transform.childCount)
        {
            _deckGroup.transform.GetChild(childIndex).GetComponent<PlayerArrowCard>().ShootPlayer();
            TurnEnded?.Invoke();
            childIndex++;
            yield return new WaitForSeconds(2);
        }
    }

    protected override void AwakeAdditionalSetup()
    {
        _startHeight = GetComponent<RectTransform>().sizeDelta.y;
        foreach (var arrow in _arrowCards)
            CreateCard(arrow);
    }

    private void CreateCard(Arrow arrow)
    {
        GameObject button = Instantiate(_prefabsList.Where(p => p.ArrowType == arrow.Type).First().Prefab, _deckGroup.transform);
        button.GetComponent<PlayerArrowCard>().SetArrow(arrow);
    }
}

