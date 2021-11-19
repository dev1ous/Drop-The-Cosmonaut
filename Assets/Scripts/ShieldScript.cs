using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        transform.Rotate(new Vector3(.5f, .5f, .5f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {   
            gm.player.haveShield = true;
            Destroy(gameObject);
        }
    }

}
