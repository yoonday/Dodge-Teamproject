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

                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
                healthSystem.ChangeHealth(-1);

                if (collision.gameObject.GetComponent<HealthSystem>() == null)
                {
                    collision.gameObject.SetActive(false);
                }


                break;

            case "Item": // �������� ��� ȿ�� ����

                AudioManager.Instance.PlaySfx(AudioManager.Sfx.Item);

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
