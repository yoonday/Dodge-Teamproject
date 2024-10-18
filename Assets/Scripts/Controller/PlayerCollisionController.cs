using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisiontroller : MonoBehaviour
{
    
    [SerializeField] private HealthSystem healthSystem; // HealthSystem ����
    [SerializeField] private PlayerStatHandler statHandler; // ���� ����
    [SerializeField] GameObject player;


    void OnCollisionEnter2D(Collision2D collision) // �Ѿ�
    {
        string tag = collision.gameObject.tag;

        switch (tag)
        {
            case "Enemy": // ��, �� �Ѿ˿� ���� ��� ü�°���
                healthSystem.ChangeHealth(-1);
                Destroy(collision.gameObject);
                break;

            case "Item": // �������� ��� ȿ�� ����

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
