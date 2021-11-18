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
    private bool isAlive = true;

    [Header("Misc")]
    [SerializeField] private Camera cam = null;
    [SerializeField] private BoxCollider ModelSize = null;
    private Touch touchInput;
    private bool isTouch = false;
    private Vector2 touchScreenPosition;
    private Vector2 onPressScreenPosition;

   
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
    }

    void Update()
    {
        if (isTouch)
        {
            Vector3 onPressPositionToWorld = cam.ScreenToWorldPoint(new Vector3(onPressScreenPosition.x, onPressScreenPosition.y, 10f));
            Vector3 touchPositionToWorld = cam.ScreenToWorldPoint(new Vector3(touchScreenPosition.x, touchScreenPosition.y, 10f));
            Vector3 directionVector = touchPositionToWorld - onPressPositionToWorld;


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

            transform.position += directionVector * moveSpeed * Time.deltaTime;
        }

        transform.position += Vector3.down * fallingSpeed * Time.deltaTime;
    }

    public void TakeDamage()
    {
        if (haveShield)
            haveShield = false;
        else
            Death();
    }

    private void Death() => isAlive = false;






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
