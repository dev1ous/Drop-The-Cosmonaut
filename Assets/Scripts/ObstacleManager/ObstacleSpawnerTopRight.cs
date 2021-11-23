using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerTopRight : MonoBehaviour
{
    [SerializeField] protected ObstacleScript[] obstacles;
    [SerializeField] protected GameManager gm;

    

    private void Update()
    {
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, .1f);
    }
}
