using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinitSea : MonoBehaviour
{
    public GameObject waterTilePrefab;  // Prefab of the water tile
    public Transform player;            // Reference to the player's ship
    public int gridSize = 3;            // Size of the grid (3x3 in this case)
    public float tileSize = 50f;        // Size of each water tile

    private GameObject[,] waterTiles;

    void Start()
    {
        waterTiles = new GameObject[gridSize, gridSize];

        // Initialize and position tiles around the player
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
    }

    void Update()
    {
        UpdateTilePositions();
    }

    public void UpdateTilePositions()
    {
        // Calculate player's grid position
        int playerGridX = Mathf.FloorToInt(player.position.x / tileSize);
        int playerGridZ = Mathf.FloorToInt(player.position.z / tileSize);

        // Reposition tiles based on player movement
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                int tileX = playerGridX + x - gridSize / 2;
                int tileZ = playerGridZ + z - gridSize / 2;

                Vector3 newTilePosition = new Vector3(tileX * tileSize, 0, tileZ * tileSize);
                waterTiles[x, z].transform.position = newTilePosition;
            }
        }
    }
}
