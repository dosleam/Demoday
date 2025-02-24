using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    public GameObject impactPrefab; // Le prefab de l'impact (cylindre aplati)
    public float impactDuration = 2f; // Durée avant que l'impact disparaisse

    private void OnCollisionEnter(Collision collision)
    {
        // Vérifie si la balle touche une cible
        if (collision.gameObject.CompareTag("Target")) // Assure-toi que tes cibles ont le tag "Target"
        {
            // Obtenir le point d'impact et la normale de la surface touchée
            ContactPoint contact = collision.contacts[0];

            // Créer l'impact à l'endroit du contact
            GameObject impact = Instantiate(impactPrefab, contact.point, Quaternion.LookRotation(contact.normal));

            // Détruire l'impact après un délai
            Destroy(impact, impactDuration);

            // Détruire la balle après l'impact
            Destroy(gameObject);
        }
    }
}
