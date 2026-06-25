using UnityEngine;

public class WeaponAim360 : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 1. Find where the mouse is in the 2D game world
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        // 2. Get the direction vector from the weapon to the mouse
        Vector2 direction = mousePosition - transform.position;

        // 3. Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 4. Apply the rotation to the pivot
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // 5. Clean Visual Flip: Prevents the gun from being upside down when aiming left
        Vector3 localScale = Vector3.one;
        if (mousePosition.x < transform.position.x)
        {
            localScale.y = -1f; // Flip vertically
        }
        else
        {
            localScale.y = 1f;  // Normal face-forward
        }
        transform.localScale = localScale;
    }
}