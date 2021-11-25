using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pattern
{
    public int[] spawnIndexs;
}

public class ObstacleManagerTwo : MonoBehaviour
{
    [SerializeField] private ObstacleScript[] obstacles;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private float randomRadius = 1f;
    [SerializeField] private float spawnDelayMin = 0.1f;
    [SerializeField] private float spawnDelayMax = 0.2f;
    [SerializeField] private Pattern[] patterns;

    private Pattern currentExecutingPatten = null;
    private int currentIndex = 0;
    private float spawnTimer = 0f;
    private float spawnDelay = 0f;

    private void Start()
    {
        currentExecutingPatten = patterns[Random.Range(0, patterns.Length)];
        spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnDelay)
        {
            spawnTimer -= spawnDelay;
            spawnDelay = Random.Range(spawnDelayMin, spawnDelayMax);

            SpawnObstacle(spawnPositions[currentExecutingPatten.spawnIndexs[currentIndex]].position);
            currentIndex++;

            if (currentIndex >= currentExecutingPatten.spawnIndexs.Length)
            {
                currentExecutingPatten = patterns[Random.Range(0, patterns.Length)];
                currentIndex = 0;
            }
        }
    }

    void SpawnObstacle(Vector3 position)
    {
        GameObject obstacleGO = obstacles[Random.Range(0, obstacles.Length)].gameObject;
        Instantiate(obstacleGO, position + new Vector3(Random.Range(-randomRadius, randomRadius), 0f, Random.Range(-randomRadius, randomRadius)), Quaternion.identity);
    }
}
