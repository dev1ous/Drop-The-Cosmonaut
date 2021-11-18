using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] private Text debug = null;
    [SerializeField] private Text buttonText = null;
    [SerializeField] private int moveType = 0;
    [SerializeField] private float moveSpeed = 10f;
    private Touch touchInput;
    private Vector2 screenPosition;
    private bool isTouch = false;
    private Vector2 screenPositionType2;
    private Vector2 screenPositionType3;

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
        screenPosition = new Vector2(960, 480);

        touchInput = new Touch();
        touchInput.Mobile.Position.performed += TouchPosition;
        touchInput.Mobile.Touch.performed += TouchPress;
        touchInput.Mobile.Touch.canceled += TouchRelease;
    }

    void Update()
    {
        buttonText.text = moveType.ToString();
        if (isTouch)
        {
            switch (moveType)
            {
                case 0:
                    Vector3 pos1 = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
                    transform.position = pos1;

                    break;

                case 1:
                    Vector3 pos2 = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
                    if ((pos2 - transform.position).magnitude > 0.5f)                    
                        transform.position += (pos2 - transform.position).normalized * moveSpeed * 5f *  Time.deltaTime;
                    
                    break;

                case 2:
                    Vector3 pos3 = Camera.main.ScreenToWorldPoint(new Vector3(screenPositionType2.x, screenPositionType2.y, 10f));
                    Vector3 pos4 = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
                    Vector3 pos5 = pos4 - pos3;
                    
                    transform.position += pos5 * moveSpeed * Time.deltaTime;

                    break;

                case 3:
                    Vector3 pos6 = Camera.main.ScreenToWorldPoint(new Vector3(screenPositionType3.x, screenPositionType3.y, 10f));
                    Vector3 pos7 = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
                    Vector3 pos8 = pos7 - pos6;

                    transform.position += pos8;
                    screenPositionType3 = screenPosition;

                    break;

                default:
                    break;
            }
        }

        debug.text = transform.position.ToString();
    }

    private void TouchPosition(InputAction.CallbackContext context)
    {
        screenPosition = context.ReadValue<Vector2>();
    }

    private void TouchPress(InputAction.CallbackContext context)
    {
        isTouch = true;
        screenPositionType2 = screenPosition;
        screenPositionType3 = screenPosition;
    }

    private void TouchRelease(InputAction.CallbackContext context)
    {
        isTouch = false;
    }

    public void SwitchMoveMode()
    {
        moveType = ((moveType + 1) % 4);
    }
}
