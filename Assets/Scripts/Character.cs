using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float fallingSpeed = 10f;
    public bool haveShield = false;
    public float traveledDistance { get; private set; } = 0f;
    private bool isAlive = true;

    [Header("Misc")]
    [SerializeField] private Camera cam = null;
    [SerializeField] private BoxCollider ModelSize = null;
    [SerializeField] private DeathMenu deathMenu = null;
    [SerializeField] private GameObject shield = null;
    [SerializeField] private ParticleSystem speedEffect = null;
    private Touch touchInput;
    private bool isTouch = false;
    private Vector2 touchScreenPosition;
    private Vector2 onPressScreenPosition;
    private Vector3 gyroValue;

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
        touchInput = new Touch();
        touchInput.Mobile.Position.performed += TouchPosition;
        touchInput.Mobile.Touch.performed += TouchPress;
        touchInput.Mobile.Touch.canceled += TouchRelease;
        Input.gyro.updateInterval = 0f;
    }

    void Update()
    {
        // get movement 
        Vector3 directionVector = Vector3.zero;
        if (Input.gyro.enabled)
        {
            gyroValue += new Vector3(Input.gyro.rotationRateUnbiased.y, 0f, -Input.gyro.rotationRateUnbiased.x);
            directionVector = gyroValue / 5f;
        }
        else if (isTouch)
        {
            Vector3 onPressPositionToWorld = cam.ScreenToWorldPoint(new Vector3(onPressScreenPosition.x, onPressScreenPosition.y, 10f));
            Vector3 touchPositionToWorld = cam.ScreenToWorldPoint(new Vector3(touchScreenPosition.x, touchScreenPosition.y, 10f));
            directionVector = touchPositionToWorld - onPressPositionToWorld;
        }

        // rotations
        if (Mathf.Abs(directionVector.x) > 0)
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, Mathf.Sign(-directionVector.x) * Mathf.Clamp(Mathf.Abs(directionVector.x) * 15f, 0.1f, 30));
        else
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0f);

        if (Mathf.Abs(directionVector.z) > 0)
            transform.localEulerAngles = new Vector3(Mathf.Sign(directionVector.z) * Mathf.Clamp(Mathf.Abs(directionVector.z) * 6f, 0.1f, 25), transform.localEulerAngles.y, transform.localEulerAngles.z);
        else
            transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y, transform.localEulerAngles.z);


        // movement claming
        if (cam.WorldToViewportPoint(transform.position + Vector3.right * (ModelSize.center.x + ModelSize.bounds.extents.x + (directionVector.x * moveSpeed * Time.deltaTime))).x > 1f ||
            cam.WorldToViewportPoint(transform.position + Vector3.right * (ModelSize.center.x + -ModelSize.bounds.extents.x + (directionVector.x * moveSpeed * Time.deltaTime))).x < 0f)
        {
            directionVector.x = 0;
        }

        if (cam.WorldToViewportPoint(transform.position + Vector3.forward * (ModelSize.center.z + ModelSize.bounds.extents.z + (directionVector.z * moveSpeed * Time.deltaTime))).y > 1f ||
            cam.WorldToViewportPoint(transform.position + Vector3.forward * (ModelSize.center.z + -ModelSize.bounds.extents.z + (directionVector.z * moveSpeed * Time.deltaTime))).y < 0f)
        {
            directionVector.z = 0;
        }

        // movement
        transform.position += directionVector * moveSpeed * Time.deltaTime;
        transform.position += Vector3.down * fallingSpeed * Time.deltaTime;
        traveledDistance += fallingSpeed * Time.deltaTime;


        shield.SetActive(haveShield);
        //speedEffect.gameObject.SetActive(speedboost variable);
        shield.transform.rotation = Quaternion.Euler(new Vector3(90f, 0f, 0f));
    }

    public void TakeDamage()
    {
        if (haveShield)
            haveShield = false;
        else
        {
            isAlive = false;
            deathMenu.gameObject.SetActive(true);
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
}
