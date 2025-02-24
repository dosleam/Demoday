using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject impactEffect; // Le prefab d'impact

    private void OnCollisionEnter(Collision collision)
    {
        // Créer un impact à l'endroit de la collision
        if (impactEffect != null)
        {
            ContactPoint contact = collision.contacts[0]; // Premier point de contact
            Quaternion rotation = Quaternion.LookRotation(contact.normal); // Aligner l'impact avec la surface
            Instantiate(impactEffect, contact.point, rotation);
        }

        // Détruire la balle après impact
        Destroy(gameObject);
    }
}
