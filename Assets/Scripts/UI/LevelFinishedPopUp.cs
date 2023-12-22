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
        System.Threading.Thread.Sleep(10);

        string winnerTag = FindObjectsOfType<PointsCounter>().OrderByDescending(c => c.Points).First().gameObject.tag;
        Debug.Log(winnerTag);
        if (winnerTag == "Player")
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
