using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [SerializeField] private Slider healthBar;

    [Header("Score")]
    [Tooltip("Points awarded to the score when this dies. Leave at 0 for the player.")]
    [SerializeField] private int scoreValue = 0;

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
        if (scoreValue > 0 && GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }

        gameObject.SetActive(false);
    }
}
