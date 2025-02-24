using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    public GameObject impactEffect; // Référence au prefab de l'impact
    public float impactForce = 10f; // Force d'impact, si tu veux appliquer une force à l'objet

    void OnCollisionEnter(Collision collision)
    {
        // Crée l'effet d'impact à la position de collision
        Vector3 impactPoint = collision.contacts[0].point;
        Quaternion impactRotation = Quaternion.FromToRotation(Vector3.forward, collision.contacts[0].normal);
        
        // Instancier l'effet d'impact
        Instantiate(impactEffect, impactPoint, impactRotation);

        // Si tu veux appliquer une force à l'objet impacté
        if (collision.rigidbody != null)
        {
            collision.rigidbody.AddForce(-collision.contacts[0].normal * impactForce, ForceMode.Impulse);
        }

        // Détruire la balle après impact
        Destroy(gameObject);
    }
}
