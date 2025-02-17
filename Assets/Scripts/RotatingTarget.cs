using UnityEngine;

public class RotatingTarget : MonoBehaviour
{
    public float rotationSpeed = 100f; // Vitesse de rotation ajustable

    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
