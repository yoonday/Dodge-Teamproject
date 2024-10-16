using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : DodgeController
{
    private Camera camera;

    protected override void Awake()
    {
        base.Awake();
        camera = Camera.main;
    }

    public void OnMove(InputValue value) 
    {
        Vector2 moveInput = value.Get<Vector2>().normalized; 
        CallMoveEvent(moveInput);  

    }

    public void OnFire(InputValue value) 
    {
        isAttacking = value.isPressed;  
    }
}
