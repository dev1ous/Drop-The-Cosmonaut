using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerBotLeft : MonoBehaviour
{
    protected void Update()
    {
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, .1f);
    }
}
