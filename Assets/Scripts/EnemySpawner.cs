using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour // script is attached to EnemySpawner object
{
    public enum ObjectType { Enemy1, Enemy2, Enemy3 } // I intend on also adding the power-up for homing missiles and creating enemy scripts.

    public Tilemap tilemap;
    public GameObject[] objectPrefabs;  // 0 = Enemy1, 1 = Enemy2, 2 = Enemy3
    public float enemy1Probability = 0.2f;
    public float enemy3Probability = 0.1f;
    public int maxObjects = 10;
    public float minWait = 2f;
    public float maxWait = 5f;
    private List<Vector3> validSpawnPositions = new List<Vector3>();
    private List<GameObject> spawnObjects = new List<GameObject>();

    void Start()
    {
        GatherValidPositions();
        StartCoroutine(SpawnObjectsIfNeeded());
    }

    private int ActiveObjectCount()
    {
        spawnObjects.RemoveAll(item => item == null);
        return spawnObjects.Count;
    }

    private IEnumerator SpawnObjectsIfNeeded()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));

            if (ActiveObjectCount() < maxObjects)
            {
                SpawnObject();
            }            
        }
    }

    private bool PositionHasObject(Vector3 positionToCheck)
    {
        return spawnObjects.Any(checkObj => checkObj && Vector3.Distance(checkObj.transform.position, positionToCheck) < 1.0f);
    }

    private ObjectType RandomObjectType()
    {
        float randomChoice = Random.value;

        if (randomChoice <= enemy3Probability)
        {
            return ObjectType.Enemy3;
        }
        else if (randomChoice <= (enemy3Probability + enemy1Probability))
        {
            return ObjectType.Enemy1;
        }
        else
        {
            return ObjectType.Enemy2;
        }
    }

    void SpawnObject()
    {
        if (validSpawnPositions.Count == 0) return;

        Vector3 spawnPosition = Vector3.zero;
        bool validPositionFound = false;
        int attempts = 0;

        while (!validPositionFound && attempts < 20)
        {
            attempts++;
            Vector3 potentialPosition = validSpawnPositions[Random.Range(0, validSpawnPositions.Count)];
            Vector3 leftPosition = potentialPosition + Vector3.left;
            Vector3 rightPosition = potentialPosition + Vector3.right;

            if (!PositionHasObject(leftPosition) && !PositionHasObject(rightPosition))
            {
                spawnPosition = potentialPosition;
                validPositionFound = true;
            }
        }

        if (validPositionFound)
        {
            ObjectType objectType = RandomObjectType();
            GameObject gameObject = Instantiate(objectPrefabs[(int)objectType], spawnPosition, Quaternion.identity);
            spawnObjects.Add(gameObject);
        }
    }

    private void GatherValidPositions()
    {
        BoundsInt boundsInt = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(boundsInt);
        Vector3 start = tilemap.CellToWorld(new Vector3Int(boundsInt.xMin, boundsInt.yMin, 0));

        for (int x = 0; x < boundsInt.size.x; x++)
        {
            for (int y = 0; y < boundsInt.size.y; y++)
            {
                TileBase tile = allTiles[x + y * boundsInt.size.x];
                if (tile != null)
                {
                    Vector3 place = start + new Vector3(x + 0.5f, y + 1f, 0);
                    validSpawnPositions.Add(place);
                }
            }
        }
    }
}