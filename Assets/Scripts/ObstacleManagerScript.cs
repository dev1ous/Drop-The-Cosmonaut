using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagerScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameManager gm;
    [SerializeField] private ObstacleScript[] obstacles;

    [SerializeField] private float speed;

    float timer;
    private float spawnDelay;

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
        spawnDelay = Random.Range(2f, 10f);

        timer += Time.deltaTime;


        if (timer >= spawnDelay)
        {
            timer = 0;
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        GameObject obstacleGO = obstacles[Random.Range(0, obstacles.Length)].gameObject;

        Instantiate(obstacleGO, transform.position, transform.rotation);
    }
}
