using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerTopMid : MonoBehaviour
{
    [SerializeField] protected ObstacleScript[] obstacles;
    [SerializeField] protected GameManager gm;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, .1f);
    }
}
