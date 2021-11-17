using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] private Text debug = null;
    private Touch touchInput;
    private Vector2 screenPosition;

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
        touchInput.Mobile.Position.performed += touchPosition;
        screenPosition = new Vector2(transform.position.x, transform.position.z);
    }

    void Update()
    {
        
    }

    private void touchPosition(InputAction.CallbackContext context)
    {
        screenPosition = context.ReadValue<Vector2>();
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));

        transform.position = pos;
        debug.text = pos.ToString();

    }
}
