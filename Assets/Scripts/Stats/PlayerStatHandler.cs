using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatHandler : MonoBehaviour
{
    // 최종 Stat 계산 로직 : 기본 Stat + 추가 Stat

    [SerializeField] private PlayerStat baseStats; // 기본 Stat
    public PlayerStat CurrentStat {  get; private set; }
    public List<PlayerStat> statModifiers = new List<PlayerStat>(); // 추가 Stat 담을 리스트 생성

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat() // 스탯 업데이트
    {
        PlayerAttackSO playerSO = null;
        if (baseStats.playerSO != null) // 공격 스탯 초기화
        {
            playerSO = Instantiate(baseStats.playerSO);
        }

        // 현재 플레이어 Stat
        CurrentStat = new PlayerStat { playerSO = playerSO };
        CurrentStat.maxHealth = baseStats.maxHealth;
        CurrentStat.speed = baseStats.speed;
        Debug.Log("기본 스피드 설정: " + CurrentStat.speed);

    }


    public void ChangeSpeedStat(float increasedSpeed, float duration) // 속도 변경
    {
        float originalSpeed = baseStats.speed; // 기본 스피드 값 저장

        // 아이템 스탯 적용
        CurrentStat.speed = increasedSpeed;
        Debug.Log("변경된 속도 확인: " + CurrentStat.speed);

        // 코루틴 사용 : 일정 시간 후 원래 속도로 돌아감
        StartCoroutine(ResetSpeed(originalSpeed, duration));
    }

    private IEnumerator ResetSpeed(float originalSpeed, float duration) // 코루틴 (원래 스피드, 아이템 적용할 시간)
    {
        yield return new WaitForSeconds(duration); // 아이템 적용 시간

        CurrentStat.speed = originalSpeed; // 기본 스피드 적용
        Debug.Log("아이템 효과 종료: " + CurrentStat.speed);
    }

}
