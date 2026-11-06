using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour // script is attached to EnemySpawner object
{
    public enum ObjectType { Enemy1, Enemy2, Enemy3 } // I intend on also adding the power-up for homing missiles and creating enemy scripts.

    public Tilemap tilemap;
    public GameObject[] objectPrefabs;  // 0 = Enemy1, 1 = Enemy2, 2 = Enemy3
    public int maxObjects = 10;
    public float minWait; 
    public float maxWait; // minWait and maxWait are changed in the inspector
    private bool isSpawning;

    private List<Vector3> validSpawnPositions = new List<Vector3>();
    private List<GameObject> spawnObjects = new List<GameObject>();
    
    private void Awake()
    {
        isSpawning = false;
    }

    void Update()
    {
        if (!isSpawning)
        {            
            float timer = Random.Range(minWait, maxWait);
            Invoke("SpawnObject", timer);
            isSpawning = true;
        }
    }

    private void GatherValidPositions()
    {
        BoundsInt boundsInt = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(boundsInt);
    }

    void SpawnObject()
    {
        int spawnPointX = Random.Range(-5, 5);
        int spawnPointY = Random.Range(10, 20);
        Vector2 spawnPosition = new Vector2(spawnPointX, spawnPointY);

        int randEnemy = Random.Range(0, objectPrefabs.Length);

        Instantiate(objectPrefabs[randEnemy], spawnPosition, Quaternion.identity);
        isSpawning = false;
    }
}
