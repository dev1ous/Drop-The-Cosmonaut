using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableScript : MonoBehaviour
{
    [SerializeField] private GameManager gm = null;
    [SerializeField] private float fuelGiven = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            gm.nbFuel += fuelGiven;
            Destroy(gameObject);
        }
    }
}
