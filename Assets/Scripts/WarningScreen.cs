using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningScreen : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadMenuAfterDelay(7f)); // Attendre 5 secondes
    }

    IEnumerator LoadMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu"); // Remplace "MainMenu" par le nom exact de ta scène de menu
    }
}
