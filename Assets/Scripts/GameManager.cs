using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Character player;
    public float nbFuel;
    public float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
    }
}
