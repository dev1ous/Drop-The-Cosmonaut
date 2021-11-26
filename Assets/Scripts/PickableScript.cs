using UnityEngine;

public class PickableScript : MonoBehaviour
{
    [SerializeField] private PickupData pickupData;
    [SerializeField] private AudioSource music;

    [HideInInspector] public Camera cam;
    [HideInInspector] public GameManager gm;

    private void Start()
    {
    }

    private void Update()
    {
        transform.Rotate(new Vector3(1, 1, 1));

        if (gameObject.transform.position.y > gm.player.transform.position.y + 10f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            music.Play();
            gm.player.AddFuel(pickupData.fuelGiven);
        }
    }
}
