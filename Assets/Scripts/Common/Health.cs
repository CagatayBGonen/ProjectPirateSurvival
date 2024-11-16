using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;    // Maksimum can
    public int currentHealth;    // Mevcut can
    public bool isPlayer;        // Oyuncu mu, düþman mý?

    private void Start()
    {
        currentHealth = maxHealth; // Caný baþta maksimum yap
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Can 0 veya altýna düþtüðünde iþlem yap
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isPlayer)
        {
            // Oyuncunun caný biterse oyun sona erer
            Debug.Log("Game Over!");
            // Burada oyun bitirme ekranýný çaðýrabilirsiniz
            if (CompareTag("Player"))
            {
                GameManager.Instance.GameOver();
            }
        }
        else
        {
            if (CompareTag("Enemy"))
            {
                WaveManager waveManager = FindObjectOfType<WaveManager>();
                waveManager.OnEnemyKilled();
            }
            // Düþman gemisi yok edilir
            Destroy(gameObject);
            Debug.Log("Enemy Destroyed");
        }
    }
}
