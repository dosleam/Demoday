using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class ScoreEntry
{
    public int score;
}

[System.Serializable]
public class ScoreList
{
    public ScoreEntry[] scores;
}

public class AddScores : MonoBehaviour
{
    public Transform leaderboardContent;
    public GameObject scoreEntryPrefab;
    public GameObject leaderboardPanel;

    private string addScorebyURL = "https://echo-shot-vr.alwaysdata.net/echo-shot-vr_scores/add_score.php";
    private string getScoresbyURL = "https://echo-shot-vr.alwaysdata.net/echo-shot-vr_scores/get_scores.php";

    public void ShowLeaderboard()
    {
        leaderboardPanel.SetActive(true);
        GetScores();
    }

    public void CloseLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }

    public void SendScore(int score)
    {
        StartCoroutine(SendScoreCoroutine(score));
    }

    public void GetScores()
    {
        Debug.Log("Chargement des scores...");
        StartCoroutine(GetScoresCoroutine());
    }

    IEnumerator SendScoreCoroutine(int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post(addScorebyURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Score envoyé avec succès : " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Erreur lors de l'envoi du score : " + www.error);
            }
        }
    }

    IEnumerator GetScoresCoroutine()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(getScoresbyURL))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Erreur en récupérant les scores : " + www.error);
                yield break;
            }

            string json = www.downloadHandler.text;
            Debug.Log("Réponse du serveur : " + json);

            try
            {
                ScoreList scoreList = JsonUtility.FromJson<ScoreList>(json);

                if (scoreList == null || scoreList.scores == null || scoreList.scores.Length == 0)
                {
                    Debug.LogWarning("Aucun score trouvé.");
                    yield break;
                }

                var sortedScores = scoreList.scores
                                    .OrderByDescending(s => s.score)
                                    .Take(10)
                                    .ToArray();

                // Nettoyage du leaderboard existant
                foreach (Transform child in leaderboardContent)
                {
                    Destroy(child.gameObject);
                }

                // Création des nouvelles entrées
                foreach (ScoreEntry entry in sortedScores)
                {
                    GameObject newEntry = Instantiate(scoreEntryPrefab, leaderboardContent);
                    TextMeshProUGUI[] texts = newEntry.GetComponentsInChildren<TextMeshProUGUI>();

                    if (texts.Length > 0)
                        texts[0].text = entry.score.ToString();
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Erreur lors du parsing JSON : " + e.Message);
            }
        }
    }
}
