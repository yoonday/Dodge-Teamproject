using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisiontroller : MonoBehaviour
{
    
    [SerializeField] private HealthSystem healthSystem; // HealthSystem 참조
    [SerializeField] private PlayerStatHandler statHandler; // 스탯 참조
    [SerializeField] GameObject player;


    void OnCollisionEnter2D(Collision2D collision) // 총알
    {
        string tag = collision.gameObject.tag;

        switch (tag)
        {
            case "Enemy": // 적, 적 총알에 맞을 경우 체력감소
                healthSystem.ChangeHealth(-1);
                Destroy(collision.gameObject);
                break;

            case "Item": // 아이템일 경우 효과 적용

                ItemStat item = collision.gameObject.GetComponent<ItemStat>();
                if (item != null)
                {
                    item.ApplyItemEffect(healthSystem, statHandler, player);
                    Destroy(collision.gameObject);
                }
                break;

        }
    }
}
