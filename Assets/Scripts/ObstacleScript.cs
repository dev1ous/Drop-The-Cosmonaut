using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [HideInInspector] public GameManager gm;

    private void Update()
    {
        transform.Rotate(new Vector3(30f, 30f, 30f) * Time.unscaledDeltaTime);

        if (gameObject.transform.position.y > gm.player.transform.position.y + 10f)
            Destroy(gameObject);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            gm.player.TakeDamage();
    }
}
