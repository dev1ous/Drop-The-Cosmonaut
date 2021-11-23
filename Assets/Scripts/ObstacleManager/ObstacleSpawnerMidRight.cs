using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerMidRight : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, .1f);
    }
}
