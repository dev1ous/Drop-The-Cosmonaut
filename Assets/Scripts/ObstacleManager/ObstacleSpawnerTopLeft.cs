using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerTopLeft : MonoBehaviour
{
    [SerializeField] protected ObstacleScript[] obstacles;
    [SerializeField] protected GameManager gm;
    [SerializeField] private SpawnersScripts spawner;

    List<GameObject> spawnObstacles;
    
    private float spawnDelay;
    private int cpt = 1 ;

    private void Start()
    {
        spawnObstacles.Add(null);
        spawnObstacles.Add(obstacles[Random.Range(0, obstacles.Length)].gameObject);
        spawnObstacles.Add(obstacles[Random.Range(0, obstacles.Length)].gameObject);
        spawnObstacles.Add(obstacles[Random.Range(0, obstacles.Length)].gameObject);
        spawnObstacles.Add(null);
        spawnObstacles.Add(null);
        spawnObstacles.Add(null);
        spawnObstacles.Add(obstacles[Random.Range(0, obstacles.Length)].gameObject);
    }

    private void Update()
    {
        spawnDelay = Random.Range(0.5f, 4f);

        if (gm.timer >= spawnDelay)
        {
            gm.timer = 0;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        //Instantiate(spawnObstacles.Find(spawner.patterns[spawner.choosenPattern].GetValue(cpt)), transform.position, transform.rotation);
        //spawner.choosenPattern = Random.Range(0, spawner.patterns.Count);
    }
}
