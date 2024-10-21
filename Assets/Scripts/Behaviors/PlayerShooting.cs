using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet; // �Ѿ� ������
    public float spreadAngle = 30f; // �л� ����
    public int bulletCount = 5; // �߻��� �Ѿ� ����
    public float bulletSpeed = 10f; // �Ѿ� �ӵ�

    private DodgeController controller;
    [SerializeField] private Transform projectileSpawnPosition; // �Ѿ� �߻� ��ġ

    // TODO :: �׽�Ʈ�� ������ ��ü
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
        // ����ü(�Ѿ�)�� ���� ����Ʈ���� ȸ�� ���� ������
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
