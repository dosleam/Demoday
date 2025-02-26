using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject leaderboardPanel;
    private bool isLeaderboardVisible = false;

    public void ToggleLeaderboard()
    {
        isLeaderboardVisible = !isLeaderboardVisible;
        leaderboardPanel.SetActive(isLeaderboardVisible);
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
