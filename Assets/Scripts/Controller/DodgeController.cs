using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action OnAttackEvent;

    private float timeSinceLastAttack = float.MaxValue;
<<<<<<< HEAD
    protected bool isAttacking { get; set; }
    protected PlayerStatHandler stats {  get; set; }
=======
    protected bool isAttacking;
>>>>>>> parent of 9aaaad6 (Merge pull request #4 from yoonday/feature_yys)

    protected virtual void Awake()
    {
        stats = GetComponent<PlayerStatHandler>();
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
<<<<<<< HEAD

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
=======
>>>>>>> parent of 9aaaad6 (Merge pull request #4 from yoonday/feature_yys)
}
