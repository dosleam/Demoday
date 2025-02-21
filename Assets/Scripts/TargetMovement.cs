using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float speed = 2.0f; // Vitesse du mouvement
    public float distance = 5.0f; // Distance du déplacement

    private Vector3 startPosition; // Position de départ
    private bool movingRight = true; // Direction du mouvement

    void Start()
    {
        startPosition = transform.position; // Enregistre la position initiale
    }

    void Update()
    {
        // Calcule la nouvelle position
        float movement = speed * Time.deltaTime;

        if (movingRight)
        {
            transform.position += Vector3.right * movement;
            if (transform.position.x >= startPosition.x + distance)
            {
                movingRight = false; // Change de direction
            }
        }
        else
        {
            transform.position -= Vector3.right * movement;
            if (transform.position.x <= startPosition.x - distance)
            {
                movingRight = true; // Change de direction
            }
        }
    }
}
