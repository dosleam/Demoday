using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 lastPosition;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lastPosition = transform.position;
        lineRenderer.SetPosition(0, lastPosition);
        lineRenderer.SetPosition(1, lastPosition);
    }

    void Update()
    {
        lineRenderer.SetPosition(0, lastPosition);
        lineRenderer.SetPosition(1, transform.position);
        lastPosition = transform.position;
    }
}
