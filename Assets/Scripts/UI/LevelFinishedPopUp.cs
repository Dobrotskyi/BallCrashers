using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelFinishedPopUp : MonoBehaviour
{
    public static bool Finished = false;
    [SerializeField] private GameObject _passedBody;
    [SerializeField] private GameObject _failedBody;
    [SerializeField] private TextMeshProUGUI _coinsRewardField;

    private void Awake()
    {
        Finished = false;
        EndLevelPortal.Finished += OnGameOver;
        PlayerDeck.NoCards += OnGameOver;
    }

    private void OnDestroy()
    {
        EndLevelPortal.Finished -= OnGameOver;
        PlayerDeck.NoCards -= OnGameOver;
    }

    private void OnGameOver()
    {
        StartCoroutine(ShowResults());
    }

    private IEnumerator ShowResults()
    {
        yield return new WaitForEndOfFrame();
        var pointTable = FindObjectsOfType<PointsCounter>().OrderByDescending(c => c.Points).ToArray();
        var firstPlace = pointTable[0];
        var secondPlace = pointTable[1];

        if ((firstPlace.gameObject.CompareTag("Player") && firstPlace.Points > 0) && (firstPlace.Points != secondPlace.Points))
        {
            _passedBody.SetActive(true);
            _coinsRewardField.text = "+" + PlayerInfoHolder.REWARD;
            PlayerInfoHolder.AddCoins(PlayerInfoHolder.REWARD);
            PlayerInfoHolder.LevelIsPassed();
        }
        else
            _failedBody.SetActive(true);
    }
}
