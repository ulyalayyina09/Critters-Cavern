using UnityEngine;

public class BulletPSTL : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;

    void Start()
    {
        // Destroy the bullet automatically after 'lifeTime' seconds so it doesn't cause lag
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // Move forward relative to the direction the bullet is pointed
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // This is where you will handle hitting enemies later!
        // if (collision.CompareTag("Environment")) // Make sure your tilemap/walls have the "Environment" tag
        //{
        //    Destroy(gameObject);
        //}
    }
}