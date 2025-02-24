using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public Transform playerCamera; // Référence à la caméra du joueur
    public float distanceFromPlayer = 2f; // Distance à laquelle le menu apparaîtra devant le joueur
    private bool isPaused = false;

    void Update()
    {
        // Vérifie si le bouton Meta de la manette gauche est pressé
        if (Input.GetKeyDown(KeyCode.Escape) || IsMetaButtonPressed())
        {
            TogglePauseMenu();
        }

        // Si le menu est actif, il suit la caméra
        if (isPaused)
        {
            FollowPlayerView();
        }
    }

    private bool IsMetaButtonPressed()
    {
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        if (leftController.TryGetFeatureValue(CommonUsages.menuButton, out bool isPressed))
        {
            return isPressed;
        }
        return false;
    }

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenuPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;

        if (isPaused)
        {
            FollowPlayerView(); // Positionner le menu devant le joueur dès son ouverture
        }
    }

    private void FollowPlayerView()
    {
        // Positionne le menu à une certaine distance devant la caméra du joueur
        pauseMenuPanel.transform.position = playerCamera.position + playerCamera.forward * distanceFromPlayer;
        // Oriente le menu pour qu'il fasse face au joueur
        pauseMenuPanel.transform.rotation = Quaternion.LookRotation(playerCamera.forward);
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
