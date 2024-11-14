using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitSea : MonoBehaviour
{
    public GameObject waterTilePrefab;      // Deniz par�as� prefab'i
    public Transform player;                // Oyuncunun transform'u
    public int gridSize = 3;                // Grid boyutu (3x3)
    public float tileSize = 50f;            // Her deniz par�as�n�n boyutu

    private GameObject[,] waterTiles;       // Deniz par�alar�n�n tutuldu�u iki boyutlu dizi
    private Vector2 playerGridPosition;     // Oyuncunun mevcut grid pozisyonu

    void Start()
    {
        // Deniz par�alar�n� grid'e yerle�tirme
        waterTiles = new GameObject[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Vector3 tilePosition = new Vector3(
                    (x - gridSize / 2) * tileSize,
                    0,
                    (z - gridSize / 2) * tileSize
                );
                waterTiles[x, z] = Instantiate(waterTilePrefab, tilePosition, Quaternion.identity);
            }
        }

        // �lk oyuncu pozisyonunu grid olarak ayarlama
        playerGridPosition = new Vector2(
            Mathf.FloorToInt(player.position.x / tileSize),
            Mathf.FloorToInt(player.position.z / tileSize)
        );
    }

    void Update()
    {
        UpdateOceanTiles();
    }

    void UpdateOceanTiles()
    {
        // Oyuncunun yeni grid pozisyonunu hesaplama
        Vector2 newGridPosition = new Vector2(
            Mathf.FloorToInt(player.position.x / tileSize),
            Mathf.FloorToInt(player.position.z / tileSize)
        );

        // Oyuncu grid pozisyonunu de�i�tirirse deniz par�alar�n� yenile
        if (newGridPosition != playerGridPosition)
        {
            playerGridPosition = newGridPosition;

            // T�m deniz par�alar�n� yeni pozisyonlara g�re g�ncelle
            for (int x = 0; x < gridSize; x++)
            {
                for (int z = 0; z < gridSize; z++)
                {
                    int tileX = (int)playerGridPosition.x + x - gridSize / 2;
                    int tileZ = (int)playerGridPosition.y + z - gridSize / 2;

                    Vector3 newTilePosition = new Vector3(tileX * tileSize, 0, tileZ * tileSize);
                    waterTiles[x, z].transform.position = newTilePosition;
                }
            }
        }
    }
}
