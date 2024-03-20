using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLevelGeneration : MonoBehaviour
{
    [Header("Map settings")]
    public float tileSize;
    public int mapWidth, mapHeight;

    [Header("Foliage Settings")]
    public GameObject[] grassPrefabs;
    public GameObject[] treePrefabs;
    [Range(0f, 1f)]
    public float treeDencity;
    public float treeSize;
    public LayerMask treeLayerMask;

    private void Start()
    {
        float xOffset = 0;
        float yOffset = 0;

        int xTileOffset = 0;
        int yTileOffset = 0;

        if (mapWidth % 2 == 0)
        {
            xOffset = .5f;
            xTileOffset = 0;
        }
        else if (mapWidth % 2 == 1)
        {
            xOffset = 0;
            xTileOffset = 1;
        }

        if (mapHeight % 2 == 0)
        {
            yOffset = .5f;
            yTileOffset = 0;
        }
        else if (mapHeight % 2 == 1)
        {
            yOffset = 0;
            yTileOffset = 1;
        }

        for (int x = -(int)(mapWidth * tileSize / 2); x < mapWidth * tileSize / 2 + xTileOffset; x++)
        {
            for (int y = -(int)(mapHeight * tileSize / 2); y < mapHeight * tileSize / 2 + yTileOffset; y++)
            {
                GameObject newGrass = Instantiate(grassPrefabs[Random.Range(0, grassPrefabs.Length)], new Vector2(x, y), Quaternion.identity);
                newGrass.transform.localScale *= tileSize;
            }
        }

        for (int i = 0; i < (treeDencity * (mapWidth * mapHeight)) / treeSize; i++)
        {
            SpawnNewTree();
        }
    }

    void SpawnNewTree()
    {
        float x = Random.Range(-((mapWidth * tileSize) / 2), (mapWidth * tileSize) / 2);
        float y = Random.Range(-((mapHeight * tileSize) / 2), (mapHeight * tileSize) / 2);

        Vector2 spawnPos = new Vector2(x, y);
        int tries = 0;

        while (tries < 200 && Physics2D.OverlapCircle(spawnPos, treeSize + 1, treeLayerMask))
        {
            x = Random.Range(-((mapWidth * tileSize) / 2), (mapWidth * tileSize) / 2);
            y = Random.Range(-((mapHeight * tileSize) / 2), (mapHeight * tileSize) / 2);

            spawnPos = new Vector2(x, y);

            tries++;
        }

        if (tries < 200)
        {
            GameObject newTree = Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)], spawnPos, Quaternion.identity);
            newTree.transform.localScale *= treeSize;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        float xOffset = 0;
        float yOffset = 0;

        int xTileOffset = 0;
        int yTileOffset = 0;

        if (mapWidth % 2 == 0)
        {
            xOffset = .5f;
            xTileOffset = 0;
        }
        else if (mapWidth % 2 == 1)
        {
            xOffset = 0;
            xTileOffset = 1;
        }

        if (mapHeight % 2 == 0)
        {
            yOffset = .5f;
            yTileOffset = 0;
        }
        else if (mapHeight % 2 == 1)
        {
            yOffset = 0;
            yTileOffset = 1;
        }

        for (int x = -(mapWidth / 2); x < mapWidth / 2 + xTileOffset; x++)
        {
            for (int y = -(mapHeight / 2); y < mapHeight / 2 + yTileOffset; y++)
            {
                Gizmos.DrawWireCube(new Vector3(x * tileSize + xOffset, y * tileSize + yOffset, 0), new Vector3(tileSize, tileSize));
            }
        }
    }
}
