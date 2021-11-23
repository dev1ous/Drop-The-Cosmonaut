using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersScripts : MonoBehaviour
{
    [SerializeField] protected GameManager gm;
    [SerializeField] protected ObstacleScript[] obstacles;

    [SerializeField] protected float offset;

    int[] pattern = { 1, 2, 3, 4, 5, 6, 7, 8 };
    int[] pattern2 = { 5, 2, 3, 1, 8, 6, 7, 4 };
    int[] pattern3 = { 8, 5, 1, 3, 4, 2, 7, 6 };

    public List<int[]> patterns;

    public int choosenPattern = 0;

    private void Start()
    {
        patterns.Add(pattern);
        patterns.Add(pattern2);
        patterns.Add(pattern3);
    }

    private void Update() => transform.position = new Vector3(gm.player.transform.position.x, gm.player.transform.position.y - offset, gm.player.transform.position.z);
}
