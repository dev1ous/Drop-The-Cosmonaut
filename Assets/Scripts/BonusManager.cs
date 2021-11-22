using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private Camera cam;
    [SerializeField] private ShieldScript shield;
    [SerializeField] private float offset;

    [SerializeField] private float spawnBonusDistance;
    private float lastSpawnDistance;

    Vector3 pos;

    private void Start()
    {
        shield.gm = this.gm;

        transform.position = new Vector3(gm.player.transform.position.x, gm.player.transform.position.y - offset, gm.transform.position.z);
        SpawnBonus();
    }
    private void Update()
    {
        float viewportOffset = cam.transform.position.y - gm.player.transform.position.y;
        pos = new Vector3(Random.Range(cam.ViewportToWorldPoint(new Vector3(0.3f, 0, viewportOffset)).x, cam.ViewportToWorldPoint(new Vector3(1, 1, viewportOffset)).x),
                          gm.player.transform.position.y - offset,
                          Random.Range(cam.ViewportToWorldPoint(new Vector3(0f, 0, viewportOffset)).z, cam.ViewportToWorldPoint(new Vector3(1, 1, viewportOffset)).z));

        transform.position = pos;

        if (gm.player.traveledDistance - lastSpawnDistance >= spawnBonusDistance)
        {
            SpawnBonus();
            lastSpawnDistance = gm.player.traveledDistance;
        }
    }

    void SpawnBonus() => Instantiate(shield.gameObject, transform.position, transform.rotation);
}
