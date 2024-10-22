using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private PlayerStatHandler playerStatHandler; // ������ HeathSystem���� �����ϴ� ���� ������ ��


    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action Oninvincibility;
     
    private float timeSinceLastChange = float.MaxValue;
    public int CurrentHealth { get; private set; }
    private bool isAttacked = false; // ��� ����ϴ���?

    private void Awake()
    {
        playerStatHandler = GetComponent<PlayerStatHandler>();
    }

    private void Start()
    {

        if (playerStatHandler != null) // �ʱ� ���� ����
        {
            CurrentHealth = playerStatHandler.CurrentStat.maxHealth;
        }
        else
        {
            Debug.LogWarning("PlayerStatHandler ���� �ʿ�");
        }
    }

    public bool ChangeHealth(int change)
    {

        CurrentHealth += change; // ü�¿� �� ��ȭ

        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, playerStatHandler.CurrentStat.maxHealth);

        Debug.Log($"���� ü�� {CurrentHealth}");
        Debug.Log($"��ȭ ü�� {change}");

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
