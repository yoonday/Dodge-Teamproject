using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Health,  // 체력 회복 아이템
    Speed    // 속도 증가 아이템
}

public class ItemStat : MonoBehaviour
{
    public ItemType itemType; // 아이템 종류 설정
    public int value = 10;    // 회복 또는 증가할 값

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어와 충돌한 경우, 아이템 효과 적용

        }    
    }
}
