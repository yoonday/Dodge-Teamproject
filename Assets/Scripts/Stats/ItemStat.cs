using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Health,  // ü�� ȸ�� ������
    Speed,    // �ӵ� ���� ������
    Shield // �� Ȱ��ȭ ������
}

public class ItemStat : MonoBehaviour
{
    public ItemType itemType; // ������ ���� ����
    public int health = 1;    // ȸ�� 
    public float speed = 20; // ���� ���ǵ� ���� ����
    public float ItemDuration = 5f;


    public void ApplyItemEffect(HealthSystem healthSystem, PlayerStatHandler statHandler, GameObject player, bool persistent) // ȿ�� ����
    {
        switch (itemType)
        {
            case ItemType.Health:
                healthSystem.ChangeHealth(health); // ü�� ȸ��
                break;

            case ItemType.Speed:
                if(persistent)
                statHandler.ChangeSpeedStat(speed, ItemDuration); // �ӵ� ����, 5�� ���� ���ӵ�
              
                break;

            case ItemType.Shield:

                if (!persistent)
                {

                    PlayerShieldSystem shieldController = player.GetComponent<PlayerShieldSystem>();
                    if (shieldController != null)
                    {

                        shieldController.SetShieldReady(); // �� Ȱ��ȭ ��û

                    }

                }

                break;

        }
    }

}
