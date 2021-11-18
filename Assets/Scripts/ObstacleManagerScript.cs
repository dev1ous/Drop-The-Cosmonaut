using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagerScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameManager gm;
    [SerializeField] private ObstacleScript[] obstacles;

    [SerializeField] private float speed;
    [SerializeField] private float offset;

    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    float timer;
    private float spawnDelay;

    private void Start()
    {
        start.parent = null;
        end.parent = null;

        foreach (ObstacleScript obs in obstacles)
        {
            obs.gm = this.gm;
            obs.cam = this.cam;
        }
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, gm.player.transform.position.y + offset, transform.position.z);

        start.transform.position = new Vector3(start.transform.position.x, gm.player.transform.position.y + offset, start.transform.position.z);
        end.transform.position = new Vector3(end.transform.position.x, gm.player.transform.position.y + offset, end.transform.position.z);

        spawnDelay = Random.Range(2f, 10f);
        SpawnerPingPong();

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

    void SpawnerPingPong()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(start.position, end.position, time);
    }
}
