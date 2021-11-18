using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject target = null;
    private float differenceWithTarget;

    private void Start()
    {
        differenceWithTarget = transform.position.y - target.transform.position.y;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, target.transform.position.y + differenceWithTarget, transform.position.z);
    }
}
