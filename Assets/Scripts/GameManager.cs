using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public AddScores addScores;

    private int score = 0;
    private int bestScore = 0;
    private float countdownTime = 30f;
    private bool isCountdownActive = false;

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("Best Score", 0);
        UpdateScoreUI();
        UpdateTimerUI();

        if (addScores == null)
        {
            addScores = FindObjectOfType<AddScores>();
        }
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

    // Envoi du score à la base de données quand le timer est terminé
    if (!isCountdownActive && addScores != null)
    {
        addScores.SendScore(score);
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

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("Best Score", bestScore);
            PlayerPrefs.Save();

            addScores.SendScore(bestScore);
        }
    }
}
