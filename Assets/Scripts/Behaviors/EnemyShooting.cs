using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private GameObject projectilePrefab;

    private DodgeEnemyController controller;
    private EnemyAttackSO enemyAttack;

    private readonly float distance_Straight = 0.5f;


    private void Awake()
    {
        controller = GetComponent<DodgeEnemyController>();
    }

    void Start()
    {
        controller.OnAttackEvent += StartAttackCoroutine;
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
                case EnemyAttackSO.AttackType.STRAIGHT:
                    float startPosX = projectileSpawnPosition.position.x;

                    if (enemyAttack.amount % 2 == 0) { startPosX -= distance_Straight / 2; }
                    int setStartPos = (enemyAttack.amount - 1) / 2;
                    startPosX -= distance_Straight * setStartPos;

                    for (int j = 0; j < enemyAttack.amount; j++)
                    {
                        Vector2 spawnPosition = new(startPosX + (distance_Straight * j), projectileSpawnPosition.position.y);

                        var projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

                        projectile.transform.rotation = transform.rotation;
                        projectile.GetComponent<EnemyProjectileController>().Init(transform.TransformDirection(Vector2.up).normalized, enemyAttack);
                    }
                    break;

                case EnemyAttackSO.AttackType.ANGLED:
                    float startThetaAngled = (180 - enemyAttack.angleDeg) / 2;
                    float thetaAngled = enemyAttack.angleDeg / (enemyAttack.amount - 1);

                    for (int j = 0; j < enemyAttack.amount; j++)
                    {
                        var projectile = Instantiate(projectilePrefab, projectileSpawnPosition.position, Quaternion.identity);

                        projectile.transform.rotation = transform.rotation;

                        float curTheta = startThetaAngled + (thetaAngled * j);
                        float cos = Mathf.Cos(curTheta * Mathf.Deg2Rad);
                        float sin = Mathf.Sin(curTheta * Mathf.Deg2Rad);

                        projectile.GetComponent<EnemyProjectileController>().Init(transform.TransformDirection(new(cos, sin)).normalized, enemyAttack);
                    }
                    break;

                case EnemyAttackSO.AttackType.SPREAD:

                    float startThetaSpread;
                    if (i % 2 != 0)
                    {
                        startThetaSpread = (180 - enemyAttack.angleDeg) / 2 + enemyAttack.angleDeg;
                    }
                    else
                    {
                        startThetaSpread = (180 - enemyAttack.angleDeg) / 2;
                    }
                    float thetaSpread = enemyAttack.angleDeg / (enemyAttack.amount - 1);

                    for (int j = 0; j < enemyAttack.amount; j++)
                    {
                        var projectile = Instantiate(projectilePrefab, projectileSpawnPosition.position, Quaternion.identity);

                        projectile.transform.rotation = transform.rotation;

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

                        projectile.GetComponent<EnemyProjectileController>().Init(transform.TransformDirection(new(cos, sin)).normalized, enemyAttack);

                        yield return new WaitForSeconds(enemyAttack.spreadDelay);
                    }
                    break;
            }
        }

        yield return new WaitForSeconds(enemyAttack.delay);
    }
}
