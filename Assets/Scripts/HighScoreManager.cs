using System.Linq;
using UnityEngine;
using TMPro; // Ajoute ce namespace pour TextMeshPro

public class HighScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;      // Score actuel (à gauche)
    public TextMeshProUGUI highScoresText; // Affichage des meilleurs scores (à droite)

    private int currentScore = 0;
    private float[] bestScores = new float[6]; // 6 meilleurs scores

    void Start()
    {
        LoadScores(); // Charger les scores sauvegardés
        UpdateHighScoresDisplay();
    }

    void Update()
    {
        scoreText.text = "Score : " + currentScore;
    }

    // Ajouter des points au score
    public void AddScore(int amount)
    {
        currentScore += amount;
    }

    // Sauvegarder le score à la fin du chrono
    public void SaveScore()
    {
        if (currentScore > 0)
        {
            bestScores[5] = currentScore; // Ajoute le score actuel
            bestScores = bestScores.OrderByDescending(s => s).ToArray(); // Trie les scores
            SaveScores(); // Sauvegarde les meilleurs scores
        }

        currentScore = 0; // Réinitialise le score
        UpdateHighScoresDisplay();
    }

    // Sauvegarder les meilleurs scores avec PlayerPrefs
    void SaveScores()
    {
        for (int i = 0; i < bestScores.Length; i++)
        {
            PlayerPrefs.SetFloat("HighScore" + i, bestScores[i]);
        }
        PlayerPrefs.Save();
    }

    // Charger les scores sauvegardés
    void LoadScores()
    {
        for (int i = 0; i < bestScores.Length; i++)
        {
            bestScores[i] = PlayerPrefs.GetFloat("HighScore" + i, 0);
        }
    }

    // Afficher les 6 meilleurs scores
    void UpdateHighScoresDisplay()
    {
        highScoresText.text = "Meilleurs Scores\n";
        for (int i = 0; i < bestScores.Length; i++)
        {
            highScoresText.text += $"{i + 1}. {bestScores[i]:F4}\t";
            if ((i + 1) % 3 == 0) highScoresText.text += "\n"; // Retour à la ligne toutes les 3 entrées
        }
    }
}
