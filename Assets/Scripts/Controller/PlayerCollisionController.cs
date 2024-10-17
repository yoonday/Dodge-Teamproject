using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisiontroller : MonoBehaviour
{
    
    [SerializeField] private HealthSystem healthSystem; // HealthSystem ����


    void OnCollisionEnter2D(Collision2D collision) // �Ѿ�
    {
        // ���� ȭ�쿡 �¾��� ���
        if (collision.gameObject.CompareTag("Enemy"))
        {
            healthSystem.ChangeHealth(-1);
            Destroy(collision.gameObject); // �Ѿ� �ı�
        }
    }


}
