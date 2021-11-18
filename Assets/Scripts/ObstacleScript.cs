using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public Camera cam;
    public GameManager gm;

    private void Update()
    {
        //Si l'objet est derriere le player et qu'il n'est plus daans le champs de vision de la camera
        //alors on  detruit
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            gm.player.TakeDamage();
            Destroy(gameObject);
        }
    }
}
