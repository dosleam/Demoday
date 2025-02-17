using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Référence au VideoPlayer
    public GameObject screen;        // Référence à l'écran

    private bool isPlaying = false;  // État de la vidéo

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand")) // Vérifie si c'est une main
        {
            isPlaying = !isPlaying; // Bascule l'état

            screen.SetActive(isPlaying); // Affiche ou cache l'écran

            if (isPlaying)
                videoPlayer.Play();
            else
                videoPlayer.Stop();
        }
    }
}
