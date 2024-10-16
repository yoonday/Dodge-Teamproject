using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnAttackEvent;

    private float timeSinceLastAttack = float.MaxValue;
    protected bool isAttacking { get; set; }

    protected virtual void Awake()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleAttackDelay();
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }

    private void HandleAttackDelay()
    {
        //TODO :: 쿨 다운 시간 변수 설정(AttackSO로 대체)
        float cooldown = 0.2f;

        if (timeSinceLastAttack < cooldown)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        else if (isAttacking && timeSinceLastAttack >= cooldown)
        {
            timeSinceLastAttack = 0;
            CallAttackEvent(); // TODO :: 파라미터에 stat값 넣어주기
        }
    }
    
}
