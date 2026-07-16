using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if (player != null)
        {
            // Ambil posisi player, tapi kunci ketinggian (Z/Y) kamera minimap
            Vector3 newPosition = player.position;
            newPosition.z = transform.position.z; // Jaga jarak kamera tetap di atas
            transform.position = newPosition;
        }
    }
}