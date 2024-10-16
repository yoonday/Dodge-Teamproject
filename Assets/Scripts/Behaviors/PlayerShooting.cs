using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private DodgeController controller;
    [SerializeField] private Transform projectileSpawnPosition; // �Ѿ� �߻� ��ġ

    // TODO :: �׽�Ʈ�� ������ ��ü
    public GameObject TestPrefab; 


    private void Awake()
    {
        controller = GetComponent<DodgeController>();
    }

    void Start()
    {
        controller.OnAttackEvent += OnShoot;
    }

    private void OnShoot()
    {
        CreateProjectile();
    }

    private void CreateProjectile()
    {
        // ����ü(�Ѿ�)�� ���� ����Ʈ���� ȸ�� ���� ������
        Instantiate(TestPrefab, projectileSpawnPosition.position, Quaternion.identity); 
    }
}
