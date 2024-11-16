using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

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
    public void GameOver()
    {
        Debug.Log("Game Over!");
        // Game Over ekran�n� a��n veya sahneyi yeniden ba�lat�n
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
