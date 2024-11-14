using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitSea : MonoBehaviour
{
    public GameObject waterTilePrefab;      // Deniz parçasý prefab'i
    public Transform player;                // Oyuncunun transform'u
    public int gridSize = 3;                // Grid boyutu (3x3)
    public float tileSize = 50f;            // Her deniz parçasýnýn boyutu

    private GameObject[,] waterTiles;       // Deniz parçalarýnýn tutulduðu iki boyutlu dizi
    private Vector2 playerGridPosition;     // Oyuncunun mevcut grid pozisyonu

    void Start()
    {
        // Deniz parçalarýný grid'e yerleþtirme
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

        // Ýlk oyuncu pozisyonunu grid olarak ayarlama
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

        // Oyuncu grid pozisyonunu deðiþtirirse deniz parçalarýný yenile
        if (newGridPosition != playerGridPosition)
        {
            playerGridPosition = newGridPosition;

            // Tüm deniz parçalarýný yeni pozisyonlara göre güncelle
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
