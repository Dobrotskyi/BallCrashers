using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeck : ArrowCardsDeck
{
    public static event Action TurnEnded;
    public static event Action NoCards;

    [Serializable]
    public struct PrefabArrowTypePair
    {
        public GameObject Prefab;
        public Arrow.ArrowType ArrowType;
    }

    public bool TurnStarted { private set; get; } = false;

    [SerializeField] private List<PrefabArrowTypePair> _prefabsList;
    [SerializeField] private HorizontalLayoutGroup _deckGroup;
    [SerializeField] private HorizontalLayoutGroup _enemyDeckGroup;
    [SerializeField] private bool _startHidden = true;
    [SerializeField] private TextMeshProUGUI _deckBelongsField;
    [SerializeField] private Button _startRoundButton;

    private float _startHeight = 0;
    private bool _paused;

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

    public void ShowBasicCards()
    {
        if (!_enemyDeckGroup.gameObject.activeSelf)
            SwitchDeckGroup();

        foreach (Transform child in _enemyDeckGroup.transform)
            Destroy(child.gameObject);

        foreach (var prefab in _prefabsList.Select(p => p.Prefab))
        {
            Button button = Instantiate(prefab, _enemyDeckGroup.transform).GetComponent<Button>();
            Destroy(button.transform.Find("ChangePlaceButtons").gameObject);
            Destroy(button.GetComponent<PlayerArrowCard>());
            button.onClick.AddListener(() =>
            {
                FindObjectOfType<GiveOrderAbility>(true).SetChangedArrow(_prefabsList[button.transform.GetSiblingIndex()].ArrowType);
            });
        }
    }

    public void AllowRearrange()
    {
        TurnStarted = false;
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

    public void DisplayOtherDeck(EnemyDeck deck)
    {
        if (_enemyDeckGroup.transform.childCount != 0)
            foreach (Transform child in _enemyDeckGroup.transform)
                Destroy(child.gameObject);

        if (!_enemyDeckGroup.gameObject.activeSelf)
            SwitchDeckGroup();

        foreach (var card in deck.GetUnused())
            CreateEnemyCard(card);
    }

    public void StartTurn()
    {
        TurnStarted = true;

        if (!DeckUsed)
            StartCoroutine(MakeTurns());
    }

    private IEnumerator MakeTurns()
    {
        int childIndex = 0;
        while (childIndex < _deckGroup.transform.childCount)
        {
            if (!Ability.AbilityInUse && TurnStarted)
            {
                _deckGroup.transform.GetChild(childIndex).GetComponent<PlayerArrowCard>().ShootPlayer();
                TurnEnded?.Invoke();
                childIndex++;
            }
            yield return new WaitForSeconds(2);
        }
        NoCards?.Invoke();
    }

    protected override void AwakeAdditionalSetup()
    {
        EndLevelPortal.Finished += StopAllCoroutines;
        FindObjectOfType<GiveOrderAbility>(true).Finished += SwitchDeckGroup;

        RectTransform rectTr = GetComponent<RectTransform>();
        _startHeight = rectTr.sizeDelta.y;
        if (_startHidden)
            rectTr.sizeDelta = new Vector2(rectTr.sizeDelta.x, 0);

        foreach (var arrow in _arrowCards)
            CreateCard(arrow);
    }

    private void OnDestroy()
    {
        EndLevelPortal.Finished -= StopAllCoroutines;
        GiveOrderAbility ability = FindObjectOfType<GiveOrderAbility>(true);
        if (ability)
            ability.Finished -= SwitchDeckGroup;
    }

    private void SwitchDeckGroup()
    {
        if (Ability.AbilityInUse)
        {
            _deckGroup.gameObject.SetActive(!_deckGroup.gameObject.activeSelf);
            _enemyDeckGroup.gameObject.SetActive(!_enemyDeckGroup.gameObject.activeSelf);
            if (_deckGroup.gameObject.activeSelf)
            {
                _deckBelongsField.text = "Your deck";
                transform.GetComponentInChildren<ScrollRect>().content = _deckGroup.GetComponent<RectTransform>();
                _startRoundButton.interactable = true;
            }
            else
            {
                _deckBelongsField.text = "Select card to change";
                transform.GetComponentInChildren<ScrollRect>().content = _enemyDeckGroup.GetComponent<RectTransform>();
                _startRoundButton.interactable = false;
            }
        }
        else
        {
            if (!_deckGroup.gameObject.activeSelf)
            {
                _deckGroup.gameObject.SetActive(true);
                _enemyDeckGroup.gameObject.SetActive(false);
                _deckBelongsField.text = "Your deck";
                transform.GetComponentInChildren<ScrollRect>().content = _deckGroup.GetComponent<RectTransform>();
                _startRoundButton.interactable = true;
            }
        }
    }

    private void CreateCard(Arrow arrow)
    {
        GameObject button = Instantiate(_prefabsList.Where(p => p.ArrowType == arrow.Type).First().Prefab, _deckGroup.transform);
        button.GetComponent<PlayerArrowCard>().SetArrow(arrow);
    }

    private void CreateEnemyCard(Arrow arrow)
    {
        Button button = Instantiate(_prefabsList.Where(p => p.ArrowType == arrow.Type).First().Prefab, _enemyDeckGroup.transform).GetComponent<Button>();
        Destroy(button.transform.Find("ChangePlaceButtons").gameObject);
        Destroy(button.GetComponent<PlayerArrowCard>());
        button.onClick.AddListener(() =>
        {
            _deckBelongsField.text = "Select card to chane with";
        });
        button.AddComponent<EnemyArrowCard>().Arrow = arrow;
        if (!button.interactable)
            button.interactable = true;
    }
}

