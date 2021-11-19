using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameManager gm = null;
    [SerializeField] private PickableScript[] pickables;

    private float spawnDelay = 0f;
    private float timer = 0f;

    private void Start()
    {
        foreach (PickableScript pick in pickables)
        {
            pick.gm = this.gm;
            pick.cam = this.cam;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        spawnDelay = Random.Range(2f, 10f);

        if (timer >= spawnDelay)
        {
            timer = 0;
            SpawnPickable();
        }
    }

    void SpawnPickable()
    {
        GameObject obstacleGO = pickables[Random.Range(0, pickables.Length)].gameObject;

        Instantiate(obstacleGO, transform.position, transform.rotation);
    }
}
