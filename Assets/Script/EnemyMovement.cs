using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;          // Kecepatan gerak musuh
    [SerializeField] private float chaseRadius = 5f;     // Jarak radius musuh bisa mendeteksi player

    private Transform playerTransform;

    void Start()
    {
        // Mencari objek dengan Tag "Player" secara otomatis saat spawn
        GameObject player = GameObject.FindWithTag("Player");
        
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        // Hitung jarak antara Musuh dan Player
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        // Jika player masuk dalam radius, kejar!
        if (distanceToPlayer <= chaseRadius)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        // Gerakkan musuh menuju posisi player secara smooth
        transform.position = Vector2.MoveTowards(
            transform.position, 
            playerTransform.position, 
            speed * Time.deltaTime
        );
        
        // OPTIONAL: Biar sprite musuh menghadap ke arah player (Flip X)
        if (playerTransform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (playerTransform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // Untuk membantu visualisasi radius di Unity Editor (Garis hijau lingkaran)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}