using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;    // Maksimum can
    public int currentHealth;    // Mevcut can
    public bool isPlayer;        // Oyuncu mu, d��man m�?

    private void Start()
    {
        currentHealth = maxHealth; // Can� ba�ta maksimum yap
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Can 0 veya alt�na d��t���nde i�lem yap
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isPlayer)
        {
            // Oyuncunun can� biterse oyun sona erer
            Debug.Log("Game Over!");
            // Burada oyun bitirme ekran�n� �a��rabilirsiniz
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
            // D��man gemisi yok edilir
            Destroy(gameObject);
            Debug.Log("Enemy Destroyed");
        }
    }
}
