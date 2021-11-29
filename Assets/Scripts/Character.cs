using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float defaultFallingSpeed = 10f;
    [SerializeField] private float fallingAccel = 0.05f;
    [SerializeField] private float fallingDecrementMultiplier = 1.2f;
    [SerializeField] private float maxfallingSpeed = 200f;
    [SerializeField] private float maxFuel = 200f;
    [SerializeField] private float speedBoost = 10f;
    [SerializeField] private float fuelConsumtion = 10f;
    private float currentFallingSpeed;
    private float currentFuel;
    private bool isSpeedBoost = false;

    public bool haveShield = false;
    public float traveledDistance { get; private set; } = 0f;
    public int score { get; private set; } = 0;



    [Header("Misc")]
    [SerializeField] private Camera cam = null;
    [SerializeField] private float playerRadius = 0.4f;
    [SerializeField] private Scoreboard scoreboard = null;
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Image fuelFillBar = null;
    [SerializeField] private GameObject shield = null;
    [SerializeField] private ParticleSystem speedEffect = null;
    [SerializeField] private ParticleSystem shieldBreakEffect = null;
    [SerializeField] private AudioSource speedBoostAudio = null;
    [SerializeField] private AudioSource HurtSound = null;
    private Touch touchInput;
    private Vector2 touchScreenPosition;
    private Vector2 onPressScreenPosition;
    private Vector3 gyroValue;
    private Vector3 directionVector;
    private bool isTouch = false;
    private float speedBoostAnimationTimer = 0f;



    private void OnEnable()
    {
        touchInput.Enable();
    }

    private void OnDisable()
    {
        touchInput.Disable();
    }

    void Awake()
    {
        currentFallingSpeed = defaultFallingSpeed;

        touchInput = new Touch();
        touchInput.Mobile.Position.performed += TouchPosition;
        touchInput.Mobile.Touch.performed += TouchPress;
        touchInput.Mobile.Touch.canceled += TouchRelease;
        Input.gyro.updateInterval = 0f;
    }

    void Update()
    {
        // get movement 
        if (OptionMenu.settings.gyroEnabled)
        {
            gyroValue += new Vector3(Input.gyro.rotationRateUnbiased.y, 0f, -Input.gyro.rotationRateUnbiased.x);
            directionVector = gyroValue / 5f;
        }
        else if (isTouch)
        {
            Vector3 onPressPositionToWorld = cam.ScreenToWorldPoint(new Vector3(onPressScreenPosition.x, onPressScreenPosition.y, 8.5f));
            Vector3 touchPositionToWorld = cam.ScreenToWorldPoint(new Vector3(touchScreenPosition.x, touchScreenPosition.y, 8.5f));
            directionVector += (touchPositionToWorld - onPressPositionToWorld) * 1.15f;

            onPressScreenPosition = touchScreenPosition;
        }

        // rotations
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.Sign(directionVector.x) * Mathf.Clamp(Mathf.Abs(directionVector.x) * 15f, 0f, 40), transform.localEulerAngles.z);

        // movement claming
        if ((cam.WorldToViewportPoint(transform.position + Vector3.right * playerRadius + (Vector3.right * directionVector.x * moveSpeed * Time.deltaTime)).x > 1f && directionVector.x > 0) ||
            (cam.WorldToViewportPoint(transform.position + Vector3.left * playerRadius + (Vector3.left * directionVector.x * moveSpeed * Time.deltaTime)).x < 0f && directionVector.x < 0))
        {
            directionVector.x = 0;
        }

        if ((cam.WorldToViewportPoint(transform.position + Vector3.forward * playerRadius + (Vector3.forward * directionVector.z * moveSpeed * Time.deltaTime)).y > 1f && directionVector.z > 0) ||
            (cam.WorldToViewportPoint(transform.position + Vector3.back * playerRadius + (Vector3.back * directionVector.z * moveSpeed * Time.deltaTime)).y < 0f && directionVector.z < 0))
        {
            directionVector.z = 0;
        }


        // movement
        directionVector.y = 0f;
        transform.position += directionVector * moveSpeed * Time.deltaTime;
        transform.position += Vector3.down * currentFallingSpeed * Time.deltaTime;
        traveledDistance += currentFallingSpeed * Time.deltaTime;
        directionVector -= (directionVector * moveSpeed * Time.deltaTime) / 2.5f;
        score = (int)traveledDistance;
        currentFallingSpeed += fallingAccel * Time.deltaTime;

        currentFallingSpeed = Mathf.Clamp(currentFallingSpeed, 0f, maxfallingSpeed);
        scoreText.text = score.ToString();
        fuelFillBar.fillAmount = currentFuel / maxFuel;

        if (isSpeedBoost)
        {
            speedBoostAnimationTimer += Time.deltaTime * 1.25f;
            currentFuel -= fuelConsumtion * Time.deltaTime;

            if (currentFuel <= 0f)
            {
                currentFuel = 0f;
                isSpeedBoost = false;
                currentFallingSpeed -= speedBoost;
            }
        }
        else
        {
            speedBoostAnimationTimer -= Time.deltaTime * 1.25f;
            speedBoostAudio.Play();

        }

        speedBoostAnimationTimer = Mathf.Clamp01(speedBoostAnimationTimer);

        //anim.SetLayerWeight(anim.GetLayerIndex("Arms Layer"), speedBoostAnimationTimer);


        shield.SetActive(haveShield);
        speedEffect.gameObject.SetActive(isSpeedBoost);
        //shield.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
    }



    public void TakeDamage()
    {
        if (OptionMenu.settings.vibrationEnabled)
        {
            Vibration.Vibrate(250);
        }

        if (haveShield)
        {
            haveShield = false;
            HurtSound.Play();
            shieldBreakEffect.Play();
            currentFallingSpeed /= fallingDecrementMultiplier;
        }
        else
        {
            scoreboard.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void TouchPosition(InputAction.CallbackContext context)
    {
        touchScreenPosition = context.ReadValue<Vector2>();
    }

    private void TouchPress(InputAction.CallbackContext context)
    {
        isTouch = true;
        onPressScreenPosition = touchScreenPosition;
    }

    private void TouchRelease(InputAction.CallbackContext context)
    {
        isTouch = false;
    }

    public void AddFuel(float value)
    {
        if (isSpeedBoost == false)
        {
            currentFuel += value;
            if (currentFuel >= maxFuel)
            {
                currentFuel = maxFuel;
                isSpeedBoost = true;
                currentFallingSpeed += speedBoost;
            }
        }
    }

}
