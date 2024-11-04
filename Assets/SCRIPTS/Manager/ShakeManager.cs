using System.Collections;
using UnityEngine;

public class ShakeManager : MonoBehaviour
{
    public static ShakeManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Fonction pour activer le tremblement de caméra
    public void ShakeCamera(float intensity, float duration)
    {
        StartCoroutine(Shake(intensity, duration));
    }

    // Coroutine qui gère le tremblement de caméra
    private IEnumerator Shake(float intensity, float duration)
    {
        float elapsed = 0.0f;
        Vector3 originalPos = Camera.main.transform.localPosition;

        // Continue le tremblement tant que la durée n'est pas écoulée
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // Utilise PerlinNoise pour des mouvements plus fluides et aléatoires
            float offsetX = (Mathf.PerlinNoise(Time.time * 10f, 0f) - 0.5f) * intensity;
            float offsetY = (Mathf.PerlinNoise(0f, Time.time * 10f) - 0.5f) * intensity;

            // Applique l'offset uniquement sur les axes X et Y (2D)
            Camera.main.transform.localPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            yield return null;
        }

        // Réinitialise la position de la caméra une fois le tremblement terminé
        Camera.main.transform.localPosition = originalPos;
    }
}