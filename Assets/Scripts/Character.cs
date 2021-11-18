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

    [Header("Misc")]
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
            Vector3 onPressPositionToWorld = Camera.main.ScreenToWorldPoint(new Vector3(onPressScreenPosition.x, onPressScreenPosition.y, 10f));
            Vector3 touchPositionToWorld = Camera.main.ScreenToWorldPoint(new Vector3(touchScreenPosition.x, touchScreenPosition.y, 10f));
            Vector3 directionVector = touchPositionToWorld - onPressPositionToWorld;

            transform.position += directionVector * moveSpeed * Time.deltaTime;
        }

        transform.position += Vector3.down * fallingSpeed * Time.deltaTime;
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
