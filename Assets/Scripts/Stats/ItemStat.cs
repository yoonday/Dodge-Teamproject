using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Health,  // ü�� ȸ�� ������
    Speed    // �ӵ� ���� ������
}

public class ItemStat : MonoBehaviour
{
    public ItemType itemType; // ������ ���� ����
    public int health = 1;    // ȸ�� 
    public float speed = 2; // ���� ���ǵ� ���� ����

    public void ApplyItemEffect(HealthSystem healthSystem, PlayerStatHandler statHandler) // ȿ�� ����
    {
        switch (itemType)
        {
            case ItemType.Health:
                healthSystem.ChangeHealth(health); // ü�� ȸ��
                break;

            case ItemType.Speed:
                statHandler.ChangeSpeedStat(speed, 5f); // �ӵ� ����, 5�� ���� ���ӵ�
                break;
        }
    }

}
