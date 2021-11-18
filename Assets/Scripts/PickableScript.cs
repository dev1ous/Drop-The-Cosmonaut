using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableScript : MonoBehaviour
{
    [SerializeField] private float fuelGiven = 0f;

    private GameManager gm = null;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(1, 1, 1));    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            gm.nbFuel += fuelGiven;
            Destroy(gameObject);
        }
    }
}
