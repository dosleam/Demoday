using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    // Cette fonction sera visible dans l'Inspector pour le bouton Menu Principal
    public void RetourMenuPrincipal()
    {
        Time.timeScale = 1f; // Remet le temps normal
        SceneManager.LoadScene("MainMenu"); // Remplace par le nom de ta scène Menu
    }

    // Cette fonction sera visible dans l'Inspector pour le bouton Quitter
    public void QuitterJeu()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Permet de quitter dans l'éditeur
#endif
    }
}
