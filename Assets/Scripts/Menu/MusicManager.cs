using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance; // Pour garder une seule instance
    public AudioSource music;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Garde l'objet même en changeant de scène
            if (music != null && !music.isPlaying)
            {
                music.Play();
            }
        }
        else
        {
            Destroy(gameObject); // Si une instance existe déjà, on détruit la nouvelle
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Abonne-toi à l'événement de chargement de scène
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Désabonne-toi pour éviter les erreurs
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene") // Vérifie si la scène actuelle est SampleScene
        {
            if (music.isPlaying)
            {
                music.Stop(); // Coupe la musique
            }
        }
    }
}
