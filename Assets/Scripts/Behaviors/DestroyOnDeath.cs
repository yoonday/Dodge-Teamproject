using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += OnDeath;
    }

    void OnDeath()
    {
        /*
         *      �׾��� �� ������ �����ǰ�.
         *      ���� ���� �� ����Ʈ �ݿ�?
         *      
         *      
         *      �������� ��������� ����� ��
         *      ���� �׾��� ��
         *      
         *      ItemSpawner -> ������ ���� (����)
         *      
         *      transform.position
         */


        Destroy(gameObject);
    }
}
