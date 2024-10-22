using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Health,  // 체력 회복 아이템
    Speed,    // 속도 증가 아이템
    Shield // 방어막 활성화 아이템
}

public class ItemStat : MonoBehaviour
{
    public ItemType itemType; // 아이템 종류 설정
    public int health = 1;    // 회복 
    public float speed = 20; // 공격 스피드 증가 배율
    public float ItemDuration = 5f;


    public void ApplyItemEffect(HealthSystem healthSystem, PlayerStatHandler statHandler, GameObject player, bool persistent) // 효과 적용
    {
        switch (itemType)
        {
            case ItemType.Health:
                healthSystem.ChangeHealth(health); // 체력 회복
                break;

            case ItemType.Speed:
                if(persistent)
                statHandler.ChangeSpeedStat(speed, ItemDuration); // 속도 증가, 5초 동안 지속됨
              
                break;

            case ItemType.Shield:

                if (!persistent)
                {

                    PlayerShieldSystem shieldController = player.GetComponent<PlayerShieldSystem>();
                    if (shieldController != null)
                    {

                        shieldController.SetShieldReady(); // 방어막 활성화 요청

                    }

                }

                break;

        }
    }

}
