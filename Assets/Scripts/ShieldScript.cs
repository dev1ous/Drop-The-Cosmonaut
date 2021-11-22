using UnityEngine;
public class ShieldScript : MonoBehaviour
{
    [HideInInspector] public GameManager gm;

    private void Update()
    {
        transform.Rotate(new Vector3(.5f, .5f, .5f));

        if (gameObject.transform.position.y > gm.player.transform.position.y + 10f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.player.haveShield = true;
        }
    }
}
