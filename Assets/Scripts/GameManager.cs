using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    private int score = 0;
    private float countdownTime = 30f;
    private bool isCountdownActive = false;

    void Start()
    {
        UpdateScoreUI();
        UpdateTimerUI();
    }

    void Update()
    {
        if (isCountdownActive)
        {
            countdownTime -= Time.deltaTime;
            UpdateTimerUI();

            if (countdownTime <= 0)
            {
                isCountdownActive = false;
                EndGame();
            }
        }
    }

    public void AddScore(int points)
    {
        if (isCountdownActive)
        {
            score += points;
            UpdateScoreUI();
        }
    }

    public void StartCountdown()
    {
        if (!isCountdownActive)
        {
            isCountdownActive = true;
            countdownTime = 30f;
            score = 0;
            UpdateScoreUI();
            UpdateTimerUI();
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(countdownTime) + "s";
    }

    void EndGame()
    {
        Debug.Log("Temps écoulé ! Score final : " + score);
        // Ici, on pourrait enregistrer le score ou afficher un message de fin
    }
}
