using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

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
    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Game Over ekranýný açýn veya sahneyi yeniden baþlatýn
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
