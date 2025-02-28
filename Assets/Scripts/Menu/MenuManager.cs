using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public AddScores addScores;
    public GameObject leaderboardPanel;

    public void ToggleLeaderboard()
    {
        if (leaderboardPanel.activeSelf)
        {
            leaderboardPanel.SetActive(false);
        }
        else
        {
            leaderboardPanel.SetActive(true);
            addScores.GetScores();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Quitte le jeu
    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Arrête le jeu dans l'éditeur
    #else
        Application.Quit(); // Fonctionne dans une build
    #endif
    }
}
