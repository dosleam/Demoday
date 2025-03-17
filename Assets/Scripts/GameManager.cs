using UnityEngine;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;         // Affichage du score actuel (à gauche)
    public TextMeshProUGUI timerText;         // Affichage du temps restant
    public TextMeshProUGUI highScoresText;    // Affichage des 6 meilleurs scores (à droite)
    public AddScores addScores;               // Pour envoyer le score à la base de données

    private int score = 0;                    // Score actuel
    private float countdownTime = 30f;        // Temps du chrono
    private bool isCountdownActive = false;   // Si le chrono est actif
    private float[] bestScores = new float[6]; // Tableau pour les 6 meilleurs scores

    void Start()
    {
        LoadScores();         // Charger les meilleurs scores au démarrage
        UpdateScoreUI();      // Afficher le score initial
        UpdateTimerUI();      // Afficher le chrono
        UpdateHighScoresUI(); // Afficher les meilleurs scores

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
    }

    public void StartCountdown()
    {
        if (!isCountdownActive)
        {
            isCountdownActive = true;
            countdownTime = 30f;
            score = 0; // Réinitialise le score seulement au début du chrono
            UpdateScoreUI();
            UpdateTimerUI();
        }
    }

    void EndGame()
    {
        Debug.Log("Temps écoulé ! Score final : " + score);

        // Si le score est parmi les 6 meilleurs, on l'enregistre
        SaveHighScore(score);

        // Envoi du score à la base de données
        if (addScores != null)
        {
            addScores.SendScore(score);
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + Mathf.FloorToInt(score);
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(countdownTime) + "s";
    }

    void UpdateHighScoresUI()
    {
        highScoresText.text = "Meilleurs Scores\n";

        for (int i = 0; i < bestScores.Length; i++)
        {
            highScoresText.text += $"{i + 1}. {bestScores[i]:F4}\t";

            // Retour à la ligne toutes les 3 colonnes
            if ((i + 1) % 3 == 0) highScoresText.text += "\n";
        }
    }

    void SaveHighScore(float newScore)
    {
        // Ajoute le score et trie les meilleurs scores
        bestScores[5] = newScore; // Remplace le dernier score
        bestScores = bestScores.OrderByDescending(s => s).ToArray();

        // Sauvegarde les 6 meilleurs scores
        for (int i = 0; i < bestScores.Length; i++)
        {
            PlayerPrefs.SetFloat("HighScore" + i, bestScores[i]);
        }
        PlayerPrefs.Save();

        // Met à jour l'affichage
        UpdateHighScoresUI();
    }

    void LoadScores()
    {
        for (int i = 0; i < bestScores.Length; i++)
        {
            bestScores[i] = PlayerPrefs.GetFloat("HighScore" + i, 0);
        }
    }
}
