using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour // script is attached to EnemySpawner object
{
    public GameObject[] enemyPrefabs;
    public float minWait;
    public float maxWait;
    private bool isSpawning;

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

    void SpawnObject()
    {
        int spawnPointX = Random.Range(-10, 10);
        int spawnPointY = Random.Range(10, 20);
        Vector2 spawnPosition = new Vector2(spawnPointX, spawnPointY);

        int randEnemy = Random.Range(0, enemyPrefabs.Length);

        Instantiate(enemyPrefabs[randEnemy], spawnPosition, Quaternion.identity);
        isSpawning = false;
    }
}
