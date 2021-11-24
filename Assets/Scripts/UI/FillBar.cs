using UnityEngine.UI;
using UnityEngine;
using System;

public class FillBar : MonoBehaviour
{
    private readonly Slider slider;
    private readonly GameManager gameManager;

    private float currentValue = 0f;
    [SerializeField]
    public int maxFuelNeeded;
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = value;
            slider.value = currentValue;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        CurrentValue = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentValue = gameManager.nbFuel / maxFuelNeeded;
    }
}
