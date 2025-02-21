using UnityEngine;

public class StartTarget : MonoBehaviour
{
    public GameManager gameManager;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // Si la balle touche la cible
        {
            gameManager.StartCountdown(); // Lance le compte à rebours
        }
    }
}
