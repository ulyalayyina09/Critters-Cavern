using UnityEngine;

public class HealBuff : MonoBehaviour
{
    [SerializeField] private int healAmount = 15; // Jumlah darah yang dipulihkan

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                // Memulihkan darah player (pastikan tidak melebihi maxHealth)
                playerHealth.currentHealth = Mathf.Min(playerHealth.currentHealth + healAmount, playerHealth.maxHealth);
                
                // Update visual health bar bawaan player kamu
                if (playerHealth.healthBar != null)
                {
                    playerHealth.healthBar.value = playerHealth.currentHealth;
                }
                Destroy(gameObject); // Hancurkan item setelah diambil
            }
        }
    }
}