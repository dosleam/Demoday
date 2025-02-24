using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreValue = 0; // Valeur en points de la cible
    public bool isStartTarget = false; // Est-ce la cible de départ ?

    private void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("Bullet"))
    {
        if (isStartTarget)
        {
            FindObjectOfType<GameManager>().StartCountdown();
        }
        else
        {
            FindObjectOfType<GameManager>().AddScore(scoreValue);
        }
    }
}

}
