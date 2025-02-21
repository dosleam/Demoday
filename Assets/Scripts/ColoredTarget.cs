using UnityEngine;

public class ColoredTarget : MonoBehaviour
{
    public GameManager gameManager;
    public int points = 10; // Points donnés par cette cible

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameManager.AddScore(points);
            gameObject.SetActive(false); // Désactive la cible après l'avoir touchée
        }
    }
}
