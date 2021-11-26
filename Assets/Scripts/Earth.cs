using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    [SerializeField] private GameObject cloud = null;

    void Update()
    {
        transform.Rotate(new Vector3(1.2f, 0f, 3f) * Time.deltaTime);
        cloud.transform.Rotate(new Vector3(0.3f, 0f, 0.5f) * Time.deltaTime);
    }
}
