using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnAttackEvent;
    public event Action OnItemEvent;

    private float timeSinceLastAttack = float.MaxValue;

    protected bool isAttacking { get; set; }

    protected bool isShild{ get; set; }

    protected PlayerStatHandler stats {  get; set; }

    protected virtual void Awake()
    {
        stats = GetComponent<PlayerStatHandler>();
    }

    public void CallItemEvent()
    {
        OnItemEvent?.Invoke();
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
        if (timeSinceLastAttack < stats.CurrentStat.playerSO.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        else if (isAttacking && timeSinceLastAttack >= stats.CurrentStat.playerSO.delay)
        {
            timeSinceLastAttack = 0;
            CallAttackEvent(); 
        }
    }
}
