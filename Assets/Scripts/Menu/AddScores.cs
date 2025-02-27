using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;


[System.Serializable]
public class ScoreEntry
{
    public string pseudonyme;
    public int score;
}

[System.Serializable]
public class ScoreList
{
    public ScoreEntry[] scores;
}

public class ScoreManager : MonoBehaviour
{
    public GameObject leaderboardPanel;
    public Transform leaderboardContent;
    public GameObject scoreEntryPrefab;

    public Button closeButton;

    // Ceci ira chercher le script add_score.php
    private string addScorebyURL = "https://echo-shot-vr.alwaysdata.net/echo-shot-vr_scores/add_score.php";

    private string getScoresbyURL = "https://echo-shot-vr.alwaysdata.net/echo-shot-vr_scores/get_scores.php";

    public void SendScore(string playerID, int score)
    {
        StartCoroutine(SendScoreCoroutine(playerID, score));
    }

    public void GetScores()
    {
        StartCoroutine(GetScoresCoroutine());
    }

    void Start()
    {
        leaderboardPanel.SetActive(false);
        closeButton.onClick.AddListener(CloseLeaderboard);
    }

    public void ShowLeaderboard()
    {
        leaderboardPanel.SetActive(true);
        StartCoroutine(GetScoresCoroutine());
    }

    void CloseLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }

    IEnumerator SendScoreCoroutine(string playerID, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("pseudonyme", playerID);
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
                Debug.LogError("Erreur en récupérant les scores: " + www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                ScoreList scoreList = JsonUtility.FromJson<ScoreList>("{\"scores\":" + json + "}");

                // Ceci va trier les scores du plus grand au plus petit et les limiter à 10
                var sortedScores = scoreList.scores
                                    .OrderByDescending(s => s.score)
                                    .Take(10)
                                    .ToArray();


                foreach (Transform child in leaderboardContent)
                {
                    Destroy(child.gameObject);
                }
            
                foreach (ScoreEntry entry in scoreList.scores)
                {
                    GameObject newEntry = Instantiate(scoreEntryPrefab, leaderboardContent);
                    TextMeshProUGUI[] texts = newEntry.GetComponentsInChildren<TextMeshProUGUI>();
                    texts[0].text = entry.pseudonyme;
                    texts[1].text = entry.score.ToString();
                }
            }
        }
    }
}
