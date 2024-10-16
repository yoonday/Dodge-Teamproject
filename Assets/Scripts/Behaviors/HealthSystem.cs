using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action Oninvincibility;
     
    private float timeSinceLastChange = float.MaxValue;
    public float CurrentHealth { get; private set; }
    private bool isAttacked = false;


    public bool ChangeHealth(int change)
    {



        if (CurrentHealth <= 0f)
        {
            CallDeath();
            return true;

        }

        if (change >= 0)
        {
            OnHeal?.Invoke();
        }

        else
        {
            OnDamage?.Invoke();
            isAttacked = true;
        }

        return true;
    
    }

    private void CallDeath()
    {

        OnDeath?.Invoke();


    }


}
