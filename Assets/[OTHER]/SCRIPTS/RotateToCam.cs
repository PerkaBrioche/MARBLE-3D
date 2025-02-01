using UnityEngine;

public class RotateToCam : MonoBehaviour
{
    private Transform cameraTransform; // Référence à la caméra
    
    private void Start()
    { 
            cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (cameraTransform != null)
        {
            print("rotate");
            Vector3 directionToCamera = cameraTransform.position - transform.position;

            // Calcule la rotation souhaitée sur l'axe y uniquement
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
            
        }
    }
}