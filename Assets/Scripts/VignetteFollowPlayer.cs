using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VignetteFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Camera playerCamera;
    public Volume volume;

    private void Start()
    {
        if(volume != null && volume.profile != null)
        {
            if(volume.profile.TryGet(out Vignette vignette))
            {
                vignette.active = true;
            }
            else
            {
                Debug.LogWarning("El volumen no contiene un ajuste de Vignette.");
            }
        }
    }

    private void Update()
    {
        if (playerTransform != null && playerCamera != null && volume != null)
        {
            Vector3 playerWorldPosition = playerTransform.position;
            Vector3 playerScreenPosition = playerCamera.WorldToScreenPoint(playerWorldPosition); // Convert player position to screen position

            // Normalize screen position between 0 and 1
            Vector2 normalizedScreenPosition = new Vector2(playerScreenPosition.x / Screen.width, playerScreenPosition.y / Screen.height);

            // Adjust the center of the vignette effect to the expected range (0 to 1)
            Vector2 adjustedCenter = new Vector2(normalizedScreenPosition.x + 0f, normalizedScreenPosition.y + 0f);
            
            if (volume.profile.TryGet(out Vignette vignette)) //Get volume profile

            {
                //Update center of vignette effect
                vignette.center.value = adjustedCenter;
            }
            else
            {
                Debug.LogWarning("El volumen no contiene un ajuste de viñeta.");
            }
        }
    }
}
