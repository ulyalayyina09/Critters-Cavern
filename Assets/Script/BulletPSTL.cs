using UnityEngine;

public class BulletPSTL : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    public int damage = 10;

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
        if (collision.CompareTag("Enemy"))
        {
            Health enemyHealth = collision.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}