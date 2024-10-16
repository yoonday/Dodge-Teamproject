using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnAttackEvent;

    private float timeSinceLastAttack = float.MaxValue;
    protected bool isAttacking;

    protected virtual void Awake()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
}
