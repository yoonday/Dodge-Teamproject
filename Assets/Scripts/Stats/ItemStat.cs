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
    public int value = 10;    // ȸ�� �Ǵ� ������ ��

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾�� �浹�� ���, ������ ȿ�� ����

        }    
    }
}
