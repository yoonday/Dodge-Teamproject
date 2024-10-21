using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet; // 총알 프리팹
    public float spreadAngle = 30f; // 분산 각도
    public int bulletCount = 5; // 발사할 총알 개수
    public float bulletSpeed = 10f; // 총알 속도

    private DodgeController controller;
    [SerializeField] private Transform projectileSpawnPosition; // 총알 발사 위치

    // TODO :: 테스트용 프리팹 교체
    public GameObject PlayerBullet; 


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
        if(GameManager.Instance.currentScore >= 50f)
        {
            SpreadShoot();
        }

        else
        {
            CreateProjectile();
        }
    }

    private void CreateProjectile()
    {
        // 투사체(총알)이 스폰 포인트에서 회전 없이 생성됨
        Instantiate(PlayerBullet, projectileSpawnPosition.position, Quaternion.identity); 
    }

    public void SpreadShoot()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * (spreadAngle / (bulletCount - 1)) - spreadAngle / 2;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            GameObject PlayerBullet = Instantiate(bullet, projectileSpawnPosition.position, transform.rotation * rotation);
            Rigidbody2D rb = PlayerBullet.GetComponent<Rigidbody2D>();
            rb.velocity = PlayerBullet.transform.forward * bulletSpeed;
        }
    }
}
