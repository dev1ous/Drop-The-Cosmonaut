using UnityEngine;

public class PickableScript : MonoBehaviour
{
    [SerializeField] private PickupData pickupData;

    [HideInInspector] public Camera cam;
    [HideInInspector] public GameManager gm;

    private void Start()
    {
    }

    private void Update()
    {
        transform.Rotate(new Vector3(1, 1, 1));    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.nbFuel += pickupData.fuelGiven;
            Destroy(gameObject);
        }
    }
}
