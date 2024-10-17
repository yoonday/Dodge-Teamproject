using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private DodgeController controller;
    [SerializeField] private Transform projectileSpawnPosition; // 총알 발사 위치

    // TODO :: 테스트용 프리팹 교체
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
        // 투사체(총알)이 스폰 포인트에서 회전 없이 생성됨
        Instantiate(TestPrefab, projectileSpawnPosition.position, Quaternion.identity); 
    }
}
