using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [HideInInspector] public Camera cam;
    [HideInInspector] public GameManager gm;

    private void Update()
    {
        transform.Rotate(new Vector3(.1f, .1f, .1f));

        //Si l'objet est derriere le player et qu'il n'est plus daans le champs de vision de la camera
        //alors on  detruit
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
            gm.player.TakeDamage();
    }
}
