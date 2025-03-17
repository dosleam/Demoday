using UnityEngine; // Bibliothèque Unity pour accéder aux composants du moteur

public class BulletTrail : MonoBehaviour // Classe attachée à un GameObject pour gérer le tracé de la balle
{
    private LineRenderer lineRenderer; // Composant qui permet de dessiner des lignes dans Unity
    private Vector3 lastPosition;      // Stocke la dernière position de la balle pour tracer la ligne

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>(); // Récupère le composant LineRenderer attaché à l'objet
        lastPosition = transform.position;           // Initialise la dernière position avec la position actuelle de la balle
        lineRenderer.SetPosition(0, lastPosition);   // Définit le premier point de la ligne au début (position actuelle)
        lineRenderer.SetPosition(1, lastPosition);   // Définit le deuxième point au même endroit (pas encore de mouvement)
    }

    void Update()
    {
        lineRenderer.SetPosition(0, lastPosition);     // Met à jour le premier point (début du tracé) à l'ancienne position
        lineRenderer.SetPosition(1, transform.position); // Met à jour le deuxième point (fin du tracé) à la position actuelle
        lastPosition = transform.position;            // Stocke la position actuelle pour le prochain frame
    }
}
