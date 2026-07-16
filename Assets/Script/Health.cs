using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (CompareTag("Player"))
    {
        // Panggil fungsi yang baru kita buat tadi
        GameManager.Instance.PlayerDied(); 
    }
    else if (CompareTag("Enemy"))
    {
        // TAMBAHKAN INI: Jika yang mati Enemy, tambah skor (misal: 10 poin)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(10); 
        }
    }
        Destroy(gameObject);
    }
}
