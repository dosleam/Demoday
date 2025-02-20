using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class GunShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public InputActionReference shootAction; // Référence à l'input action XRI

    private bool isHeld = false; // Indique si l’arme est en main

    private void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable)
        {
            grabInteractable.selectEntered.AddListener(OnSelectEntered);
            grabInteractable.selectExited.AddListener(OnSelectExited);
        }
    }

    private void OnEnable()
    {
        if (shootAction != null)
            shootAction.action.performed += OnShoot;
    }

    private void OnDisable()
    {
        if (shootAction != null)
            shootAction.action.performed -= OnShoot;
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        isHeld = true;
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        isHeld = false;
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (isHeld)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        
        // Correction de l'orientation
        bullet.transform.Rotate(0, 90, 0); 

        // Vérifier et ajouter un Rigidbody si absent
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = bullet.AddComponent<Rigidbody>();
        }

        // Désactiver la gravité pour éviter que la balle tombe immédiatement
        rb.useGravity = false;

        // Appliquer la force pour que la balle avance
        rb.velocity = firePoint.forward * bulletSpeed;
    }
}
