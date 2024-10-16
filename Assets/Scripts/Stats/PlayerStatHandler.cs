using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    // ���� Stat ��� ���� : �⺻ Stat + �߰� Stat

    [SerializeField] private PlayerStat baseStats; // �⺻ Stat
    public PlayerStat CurrentStat {  get; private set; }
    public List<PlayerStat> statModifiers = new List<PlayerStat>(); // �߰� Stat ���� ����Ʈ ����

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        PlayerAttackSO playerSO = null;
        if (baseStats.playerSO != null) // PlayerStat �ʱ�ȭ
        {
            playerSO = Instantiate(baseStats.playerSO);
        }

        // ���� Stat
        CurrentStat = new PlayerStat { playerSO = playerSO };
        CurrentStat.statsChangeType = baseStats.statsChangeType;
        CurrentStat.maxHealth = baseStats.maxHealth;
        CurrentStat.speed = baseStats.speed;

    }
}
