using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    public UIManager uiManager; // UIManager referans�
    public WaveManager waveManager; // WaveManager referans�

    private void Awake()
    {
        // E�er ba�ka bir instance varsa bu nesneyi yok et
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Sahne ge�i�lerinde yok olmas�n
    }
    private void Start()
    {
        // Ba�lang��ta UI'yi g�ncelle
        uiManager.UpdateWaveText();
        uiManager.UpdateKillCountText();
    }
    
    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Game Over ekran�n� a��n veya sahneyi yeniden ba�lat�n
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // D��man �ld���nde �a�r�l�r
    public void OnEnemyDeath()
    {
        uiManager.IncrementKillCount(); // Toplam �ld�r�len d��man say�s�n� artt�r
    }

    // Yeni wave ba�lad���nda �a�r�l�r
    public void OnNextWave()
    {
        uiManager.IncrementWave(); // Wave say�s�n� artt�r
    }
}
