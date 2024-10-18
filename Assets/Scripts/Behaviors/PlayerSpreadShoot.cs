using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpreadShoot : MonoBehaviour
{
    public GameObject bullet; // �Ѿ� ������
    public float spreadAngle = 30f; // �л� ����
    public int bulletCount = 5; // �߻��� �Ѿ� ����
    public float bulletSpeed = 10f; // �Ѿ� �ӵ�

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * (spreadAngle / (bulletCount - 1)) - spreadAngle / 2;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            GameObject PlayerBullet = Instantiate(bullet, transform.position, transform.rotation * rotation);
            Rigidbody2D rb = PlayerBullet.GetComponent<Rigidbody2D>();
            rb.velocity = PlayerBullet.transform.forward * bulletSpeed;
        }
    }
}