using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text countdownText; // Pour afficher le temps
    public Text scoreText; // Pour afficher le score
    public GameObject[] coloredTargets; // Liste des cibles de couleur

    private int score = 0;
    private float timer = 0f;
    private bool isGameActive = false;

    void Update()
    {
        if (isGameActive)
        {
            timer -= Time.deltaTime;
            countdownText.text = "Time: " + Mathf.Ceil(timer).ToString();

            if (timer <= 0)
            {
                EndGame();
            }
        }
    }

    public void StartCountdown()
    {
        timer = 30f; // Définir le compte à rebours
        score = 0; // Réinitialiser le score
        isGameActive = true;

        ActivateTargets(true); // Activer les cibles colorées
    }

    public void AddScore(int points)
    {
        if (isGameActive)
        {
            score += points;
            scoreText.text = "Score: " + score;
        }
    }

    private void EndGame()
    {
        isGameActive = false;
        ActivateTargets(false); // Désactiver les cibles
        countdownText.text = "Time's Up!";
        Debug.Log("Final Score: " + score);
    }

    private void ActivateTargets(bool active)
    {
        foreach (GameObject target in coloredTargets)
        {
            target.SetActive(active);
        }
    }
}
