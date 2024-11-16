using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    public UIManager uiManager; // UIManager referansý
    public WaveManager waveManager; // WaveManager referansý

    private void Awake()
    {
        // Eðer baþka bir instance varsa bu nesneyi yok et
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Sahne geçiþlerinde yok olmasýn
    }
    private void Start()
    {
        // Baþlangýçta UI'yi güncelle
        uiManager.UpdateWaveText();
        uiManager.UpdateKillCountText();
    }
    
    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Game Over ekranýný açýn veya sahneyi yeniden baþlatýn
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Düþman öldüðünde çaðrýlýr
    public void OnEnemyDeath()
    {
        uiManager.IncrementKillCount(); // Toplam öldürülen düþman sayýsýný arttýr
    }

    // Yeni wave baþladýðýnda çaðrýlýr
    public void OnNextWave()
    {
        uiManager.IncrementWave(); // Wave sayýsýný arttýr
    }
}
