using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    
    private PlayerInput _playerInput;
    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _touchPositionAction = _playerInput.actions["TouchPosition"];
        _touchPressAction = _playerInput.actions["TouchPress"];
    }

    private void OnEnable()
    {
        _touchPressAction.performed += TouchPressed;
    }
    
    private void OnDisable()
    {
        _touchPressAction.performed -= TouchPressed;
    }
    
    private void TouchPressed(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = _touchPositionAction.ReadValue<Vector2>();
        Collider2D hit = Physics2D.OverlapPoint(touchPosition);
        
        if (hit != null)
        {
            Debug.Log("Collider detected: " + hit.GetComponent<Collider>().name);
        }
    }
}
