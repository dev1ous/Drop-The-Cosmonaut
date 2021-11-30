using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameManager gm = null;
    [SerializeField] private PickableScript pickables;
    [SerializeField] private float offset = 10f;

    private float spawnDelay = 0f;
    private float timer = 0f;

    private Vector3 pos;


    private void Start()
    {
        pickables.gm = this.gm;
        pickables.cam = this.cam;
    }

    private void Update()
    {
        float viewportOffset = cam.transform.position.y - gm.player.transform.position.y + offset;

        pos = new Vector3(Random.Range(cam.ViewportToWorldPoint(new Vector3(0.4f, .4f, viewportOffset)).x, cam.ViewportToWorldPoint(new Vector3(.6f, .6f, viewportOffset)).x),
                          gm.player.transform.position.y - offset,
                          Random.Range(cam.ViewportToWorldPoint(new Vector3(.4f, .4f, viewportOffset)).z, cam.ViewportToWorldPoint(new Vector3(.6f, .6f, viewportOffset)).z));

        transform.position = pos;

        timer += Time.deltaTime;

        if (timer >= spawnDelay)
        {
            timer = 0;
            spawnDelay = Random.Range(0f, 1f);
            SpawnPickable();
        }
    }

    void SpawnPickable()
    {
        GameObject obstacleGO = pickables.gameObject;
        Instantiate(obstacleGO, transform.position, transform.rotation);
    }
}
