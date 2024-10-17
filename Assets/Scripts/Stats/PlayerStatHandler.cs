using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    // 최종 Stat 계산 로직 : 기본 Stat + 추가 Stat

    [SerializeField] private PlayerStat baseStats; // 기본 Stat
    public PlayerStat CurrentStat { get; private set; }
    public List<PlayerStat> statModifiers = new List<PlayerStat>(); // 추가 Stat 담을 리스트 생성

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        PlayerAttackSO playerSO = null;
        if (baseStats.playerSO != null) // PlayerStat 초기화
        {
            playerSO = Instantiate(baseStats.playerSO);
        }

        // 현재 Stat
        CurrentStat = new PlayerStat { playerSO = playerSO };
        CurrentStat.statsChangeType = baseStats.statsChangeType;
        CurrentStat.maxHealth = baseStats.maxHealth;
        CurrentStat.speed = baseStats.speed;

    }
}
