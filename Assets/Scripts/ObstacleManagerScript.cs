using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagerScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameManager gm;
    [SerializeField] private ObstacleScript[] obstacles;

    [SerializeField] private float offset;

    Vector3 pos;

    float timer;
    private float spawnDelay;

    private void Start()
    {
        foreach (ObstacleScript obs in obstacles)
            obs.gm = this.gm;
    }

    private void Update()
    {
        float viewportOffset = cam.transform.position.y - gm.player.transform.position.y;
        Debug.Log(viewportOffset);
        pos = new Vector3(Random.Range(cam.ViewportToWorldPoint(new Vector3(0.3f, 0, viewportOffset)).x, cam.ViewportToWorldPoint(new Vector3(1, 1, viewportOffset)).x),
                          gm.player.transform.position.y - offset,
                          Random.Range(cam.ViewportToWorldPoint(new Vector3(0f, 0, viewportOffset)).z, cam.ViewportToWorldPoint(new Vector3(1, 1, viewportOffset)).z));

        transform.position = pos;

        spawnDelay = Random.Range(0.5f, 4f);

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
