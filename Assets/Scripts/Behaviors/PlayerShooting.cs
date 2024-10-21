using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet; // �Ѿ� ������
    public float spreadAngle = 30f; // �л� ����
    public int bulletCount; // �߻��� �Ѿ� ����
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
        if (GameManager.Instance.currentScore >= 0f && GameManager.Instance.currentScore <= 9f)
        {
            CreateProjectile();
        }

        else if (GameManager.Instance.currentScore >= 10f && GameManager.Instance.currentScore <= 19f)
        {
            for (int i = 0; i < 2; i++)
            {
                float angle = i * (spreadAngle / (2 - 1)) - spreadAngle / 2;
                Quaternion rotation = Quaternion.Euler(new Vector3(0, angle, 0));

                // ���� 
                // GameObject PlayerBullet = Instantiate(bullet, projectileSpawnPosition.position, transform.rotation * rotation);
                // �Ʒ��� ������Ʈ Ǯ��

                var PlayerBullet = GameManager.Instance.ObjectPool.SpawnFromPool("PlayerBullet");
                PlayerBullet.transform.SetPositionAndRotation(projectileSpawnPosition.position, transform.rotation * rotation);

                Rigidbody2D rb = PlayerBullet.GetComponent<Rigidbody2D>();
                rb.velocity = PlayerBullet.transform.forward * bulletSpeed;
            }
        }

        else if (GameManager.Instance.currentScore >= 20f && GameManager.Instance.currentScore <= 29f)
        {
            for (int i = 0; i < 3; i++)
            {
                float angle = i * (spreadAngle / (3 - 1)) - spreadAngle / 2;
                Quaternion rotation = Quaternion.Euler(new Vector3(0, angle, 0));

                // ���� 
                // GameObject PlayerBullet = Instantiate(bullet, projectileSpawnPosition.position, transform.rotation * rotation);
                // �Ʒ��� ������Ʈ Ǯ��

                var PlayerBullet = GameManager.Instance.ObjectPool.SpawnFromPool("PlayerBullet");
                PlayerBullet.transform.SetPositionAndRotation(projectileSpawnPosition.position, transform.rotation * rotation);

                Rigidbody2D rb = PlayerBullet.GetComponent<Rigidbody2D>();
                rb.velocity = PlayerBullet.transform.forward * bulletSpeed;
            }
        }

        else if (GameManager.Instance.currentScore >= 30f && GameManager.Instance.currentScore <= 39f)
        {
            for (int i = 0; i < 4; i++)
            {
                float angle = i * (spreadAngle / (4 - 1)) - spreadAngle / 2;
                Quaternion rotation = Quaternion.Euler(new Vector3(0, angle, 0));

                // ���� 
                // GameObject PlayerBullet = Instantiate(bullet, projectileSpawnPosition.position, transform.rotation * rotation);
                // �Ʒ��� ������Ʈ Ǯ��
                var PlayerBullet = GameManager.Instance.ObjectPool.SpawnFromPool("PlayerBullet");
                PlayerBullet.transform.SetPositionAndRotation(projectileSpawnPosition.position, transform.rotation * rotation);

                Rigidbody2D rb = PlayerBullet.GetComponent<Rigidbody2D>();
                rb.velocity = PlayerBullet.transform.forward * bulletSpeed;
            }
        }
    }

    private void CreateProjectile()
    {
        // ����ü(�Ѿ�)�� ���� ����Ʈ���� ȸ�� ���� ������

        // ���� 
        //Instantiate(PlayerBullet, projectileSpawnPosition.position, Quaternion.identity);
        // �Ʒ��� ������Ʈ Ǯ��
        var PlayerBullet = GameManager.Instance.ObjectPool.SpawnFromPool("PlayerBullet");
        PlayerBullet.transform.SetPositionAndRotation(projectileSpawnPosition.position, Quaternion.identity);

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Fire);
    }

    
}
