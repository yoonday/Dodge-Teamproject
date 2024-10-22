using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private PlayerStatHandler playerStatHandler; // 스탯을 HeathSystem에서 관리하는 것이 용이할 듯


    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action Oninvincibility;
     
    private float timeSinceLastChange = float.MaxValue;
    public int CurrentHealth { get; private set; }
    private bool isAttacked = false; // 어디서 사용하는지?

    private void Awake()
    {
        playerStatHandler = GetComponent<PlayerStatHandler>();
    }

    private void Start()
    {

        if (playerStatHandler != null) // 초기 스탯 설정
        {
            CurrentHealth = playerStatHandler.CurrentStat.maxHealth;
        }
        else
        {
            Debug.LogWarning("PlayerStatHandler 연결 필요");
        }
    }

    public bool ChangeHealth(int change)
    {

        CurrentHealth += change; // 체력에 값 변화

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, playerStatHandler.CurrentStat.maxHealth);

        Debug.Log($"현재 체력 {CurrentHealth}");
        Debug.Log($"변화 체력 {change}");

        if (CurrentHealth <= 0)
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

    public void InitHealth(int maxHealth)
    {
        CurrentHealth = maxHealth;
    }    


}
