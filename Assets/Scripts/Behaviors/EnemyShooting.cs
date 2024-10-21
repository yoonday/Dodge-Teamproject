using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private GameObject projectilePrefab;

    private DodgeEnemyController controller;
    private EnemyAttackSO enemyAttack;

    private readonly float distance_Straight = 1f;


    private void Awake()
    {
        controller = GetComponent<DodgeEnemyController>();
    }

    void Start()
    {
        controller.OnAttackEvent += StartAttackCoroutine;
    }

    public void EnemyShootingInit()
    {
        enemyAttack = controller.EnemyAttack;
    }

    private void StartAttackCoroutine()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        if (enemyAttack == null) yield break;

        for (int i = 0; i < enemyAttack.attackCountAtOnce; i++)
        {

            switch (enemyAttack.angle)
            {
                // ���� ����
                case EnemyAttackSO.AttackType.STRAIGHT:

                    float startPosX = projectileSpawnPosition.position.x; // �Ѿ� �߻� ��ġ

                    if (enemyAttack.amount % 2 == 0) { startPosX -= distance_Straight / 2; } // �� �࿡ �Ѿ� ������ ¦���̸� ������� ������ �ȵǱ� ������ ������ �� ��ŭ ������ �о���
                    int setStartPos = (enemyAttack.amount - 1) / 2; // �ƴϸ� �����ġ�� �׳� ������ �о���. setStartPos �� �󸶳� ������ ���� ����.

                    startPosX -= distance_Straight * setStartPos; // ������ �о���.

                    for (int j = 0; j < enemyAttack.amount; j++)
                    {
                        Vector2 spawnPosition = new(startPosX + (distance_Straight * j), projectileSpawnPosition.position.y); // �� �� ��ġ���� �ϳ��� ���ذ��鼭 ����

                        var projectile = GameManager.Instance.ObjectPool.SpawnFromPool("EnemyBullet");
                        projectile.transform.SetPositionAndRotation(spawnPosition, transform.rotation);

                        projectile.GetComponent<EnemyProjectileController>().Init(transform.TransformDirection(Vector2.up).normalized, enemyAttack); // �Ѿ� �߻� ���� ����.
                    }
                    break;


                // ���������� ����
                case EnemyAttackSO.AttackType.ANGLED:

                    float startThetaAngled = (180 - enemyAttack.angleDeg) / 2; // 0������ ���� ����� ��ġ. �߻� ������ �߽��� 90���� ���´ٰ� ħ.
                    float thetaAngled = enemyAttack.angleDeg / (enemyAttack.amount - 1); // �߻� ������ �߻�ü�� ���� ���� ��.

                    for (int j = 0; j < enemyAttack.amount; j++)
                    {
                        var projectile = GameManager.Instance.ObjectPool.SpawnFromPool("EnemyBullet");
                        projectile.transform.SetPositionAndRotation(projectileSpawnPosition.position, transform.rotation);

                        float curTheta = startThetaAngled + (thetaAngled * j); // ���� �������� ��Ÿ ��ŭ ������ �̵��ϸ鼭 �߻�.
                        float cos = Mathf.Cos(curTheta * Mathf.Deg2Rad);
                        float sin = Mathf.Sin(curTheta * Mathf.Deg2Rad);

                        projectile.GetComponent<EnemyProjectileController>().Init(transform.TransformDirection(new(cos, sin)).normalized, enemyAttack); // �Ѿ� ���� ����.
                    }
                    break;


                // �帣�� ����
                case EnemyAttackSO.AttackType.SPREAD:

                    // �����ʿ��� �������� �߻����� �ݴ�� �߻����� ���� (�� �� = i �� ���� ������)
                    float startThetaSpread;
                    if (i % 2 != 0)
                    {
                        startThetaSpread = (180 - enemyAttack.angleDeg) / 2 + enemyAttack.angleDeg;
                    }
                    else
                    {
                        startThetaSpread = (180 - enemyAttack.angleDeg) / 2;
                    }

                    // ���� ���ϰ� �Ȱ��� �� �߻�ü�� ���� ����
                    float thetaSpread = enemyAttack.angleDeg / (enemyAttack.amount - 1);

                    for (int j = 0; j < enemyAttack.amount; j++)
                    {
                        var projectile = GameManager.Instance.ObjectPool.SpawnFromPool("EnemyBullet");
                        projectile.transform.SetPositionAndRotation(projectileSpawnPosition.position, transform.rotation);

                        // �߻� ������ ���� ���ų� ����.
                        float curTheta;
                        if (i % 2 != 0)
                        {
                            curTheta = startThetaSpread - (thetaSpread * j);
                        }
                        else
                        {
                            curTheta = startThetaSpread + (thetaSpread * j);
                        }

                        float cos = Mathf.Cos(curTheta * Mathf.Deg2Rad);
                        float sin = Mathf.Sin(curTheta * Mathf.Deg2Rad);

                        projectile.GetComponent<EnemyProjectileController>().Init(transform.TransformDirection(new(cos, sin)).normalized, enemyAttack); // �Ѿ� ���� ����.

                        yield return new WaitForSeconds(enemyAttack.spreadDelay);
                    }
                    break;


                // ������ ����
                case EnemyAttackSO.AttackType.STAR:

                    float startThetaStar = (180 - enemyAttack.angleDeg) / 2; // ���� �Ȱ��� ���� ���� ����.
                    float thetaStar = enemyAttack.angleDeg / (enemyAttack.amount * 2); // ���� Ư���ϰ� �߻�ü ������ 2��� ����. (������ ���߿� �ļ�)
                    int middleCount = enemyAttack.attackCountAtOnce - 2; // ����� ��(������ �κ�)�� �� �Ʒ� ��(�ݴ� �κ�)�� �� �߰� ���� ����.

                    // �� ���� ���� �߻�ü ������ �ٸ���. �߰� ���� �ﰢ���� ������ ����ؾ� �ϱ� ������ 2�谡 �ȴ�.
                    int amount;
                    if (i == 0)
                    {
                        amount = enemyAttack.amount;
                    }
                    else if (i < enemyAttack.attackCountAtOnce - 1)
                    {
                        amount = enemyAttack.amount * 2;
                    }
                    else
                    {
                        amount = enemyAttack.amount + 1;
                    }


                    for (int j = 0; j < amount; j ++)
                    {
                        var projectile = GameManager.Instance.ObjectPool.SpawnFromPool("EnemyBullet");
                        projectile.transform.SetPositionAndRotation(projectileSpawnPosition.position, transform.rotation);


                        // ���� ���� ���� �߻� ������ ���Ѵ�.
                        float curTheta;
                        if (i == 0)
                        {
                            // �� ������ �ܼ��ϰ� ������ �κ��� ��ġ�� ����ָ� �ȴ�.
                            curTheta = startThetaStar + (thetaStar * (j * 2 + 1));
                        }
                        else if (i < enemyAttack.attackCountAtOnce - 1)
                        {
                            // �߰� ���� �߿��ѵ�, ���鿡 ���� �� ���� ���������� �������� ������ �� ���� �������� �������� �ϱ� ������ �������� ���� 1�� ������ �ȴ�.. ���⼭ �����ϼ��� ���� ũ�� �������ϼ��� ���� �۴�.
                            float middleCoeff;

                            if (j % 2 == 0) // �ﰢ���� ������
                            {
                                middleCoeff = 1 - (i / (float)(middleCount + 1)); // �ﰢ���� �������� ���� ���������� �������� �ϱ� ������ �����ϼ��� ������ ���ʿ� ����. �׷��Ƿ� 1�� ���� (�����ϼ��� ���� ũ�Ƿ� i = ���� ���� �� ���� Ŀ�� �Ѵ�.)
                            }
                            else // �ﰢ���� ����
                            {
                                middleCoeff = i / (float)(middleCount + 1); // �ﰢ���� ������ ���� �������� �������� �ϱ� ������ �����ϼ��� ������ �����ʿ� ����. �׷��Ƿ� �׳� ��� (�����ʰ� �ݴ�.)
                            }

                            curTheta = startThetaStar + thetaStar * (j + middleCoeff); // �̷��� ���� �߰� ���� ����� �� ĭ�� ������ ���ؼ� ���� ��Ÿ�� �����ش�.
                        }
                        else
                        {
                            curTheta = startThetaStar + 2 * j * thetaStar; // ������ ���� �ﰢ���� �غ� �κп��� �������� �̾����� �׸���. �����ϴ� ������.
                        }

                        float cos = Mathf.Cos(curTheta * Mathf.Deg2Rad);
                        float sin = Mathf.Sin(curTheta * Mathf.Deg2Rad);

                        projectile.GetComponent<EnemyProjectileController>().Init(transform.TransformDirection(new(cos, sin)).normalized, enemyAttack); // �Ѿ� ���� ����.
                    }

                    break;
            }

            yield return new WaitForSeconds(enemyAttack.delay);
        }
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Fire);
    }
}
