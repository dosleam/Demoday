using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Warning : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 2f;
    public float displayTime = 5f;

    void Start()
    {
        StartCoroutine(WarningSequence());
    }

    IEnumerator WarningSequence()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;

        yield return new WaitForSeconds(displayTime);

        timer = 0f;
        while (timer < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        AudioListener.pause = false; // Pour éviter que Unity coupe le son automatiquement
        SceneManager.LoadScene("MainMenu");
    }
}