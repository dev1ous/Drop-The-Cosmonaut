using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagerScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameManager gm;
    [SerializeField] private ObstacleScript[] obstacles;

    [SerializeField] private float spawnDelay;

    Coroutine cor = null;

    float timer;

    private void Start()
    {
        foreach (ObstacleScript obs in obstacles)
        {
            obs.gm = this.gm;
            obs.cam = this.cam;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnDelay)
        {
            timer = 0;

            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        GameObject obstacleGO = obstacles[Random.Range(0, obstacles.Length - 1)].gameObject;

        Instantiate(obstacleGO, transform.position, transform.rotation);
    }
}
