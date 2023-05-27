using System;
using System.Collections;
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
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(_touchPositionAction.ReadValue<Vector2>());
        Vector2 touchPosition = new Vector2(worldPoint.x, worldPoint.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPosition);
        //Debug.Log("Touch position: " + touchPosition);
        if (hit != null)
        {
            Tile tile = hit.GetComponent<Tile>();
            if (tile != null)
            {
                MatchingArea.I.JoinEmptySlot(tile);
            }
        }
    }
}
