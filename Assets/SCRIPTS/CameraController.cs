using System;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target; // Le Transform à suivre
    public float smoothSpeed = 0.125f; // Vitesse de l'interpolation
     public Vector3 offset; // Décalage par rapport à la cible

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z + offset.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }

    public void UpdateTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
