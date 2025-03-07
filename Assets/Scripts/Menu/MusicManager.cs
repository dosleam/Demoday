using UnityEngine;

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
}
