using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerHead; // La tête du joueur (XR Rig)

    void Update()
    {
        Vector3 targetPosition = playerHead.position + playerHead.forward * 2f; // 2 mètres devant la tête
        transform.position = targetPosition;

        // Toujours face au joueur
        transform.LookAt(playerHead);
        transform.Rotate(0, 180, 0); // Inverser pour que le texte soit lisible
    }
}
