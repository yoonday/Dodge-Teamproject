using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : DodgeController
{
    private Camera camera;

    protected override void Awake()
    {
        camera = Camera.main;
    }

    public void OnMove(InputValue value) 
    {
        Vector2 moveInput = value.Get<Vector2>().normalized; 
        CallMoveEvent(moveInput);  

    }

    public void OnFire(InputValue value) 
    {
        // TODO :: TopDownController 내용 수정 protected bool IsAttacking { get; set; }
        // IsAttacking = value.isPressed;  
    }
}
