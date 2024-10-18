using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private EnemyAttackSO enemyAttackSO;

    private DodgeController controller;
    private EnemyAttackSO enemyAttack;

    private readonly float distance_Straight = 0.5f;


    private void Awake()
    {
        controller = GetComponent<DodgeController>();
        enemyAttack = (controller as DodgeEnemyController).EnemyAttack;
    }

    void Start()
    {
        controller.OnAttackEvent += StartAttackCoroutine;
    }

    private void StartAttackCoroutine()
    {
        StartCoroutine(AttackCoroutine());
    }

    private void OnShot()
    {
        if (enemyAttack.angle == EnemyAttackSO.Angle.STRAIGHT)
        {
            float startPosX = projectileSpawnPosition.position.x;

            if(enemyAttack.amount % 2 == 0) { startPosX -= distance_Straight / 2; }
            int setStartPos = (enemyAttack.amount - 1) / 2;
            startPosX -= distance_Straight * setStartPos;

            for(int i = 0; i < enemyAttack.amount; i ++)
            {
                Vector2 spawnPosition = new(startPosX + (distance_Straight * i), projectileSpawnPosition.position.y);

                var projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

                projectile.transform.rotation = transform.rotation;
                projectile.GetComponent<EnemyProjectileController>().Init(transform.TransformDirection(Vector2.up).normalized, enemyAttackSO);
            }

        }
        else
        {
            float startTheta = (180 - enemyAttackSO.angleDeg) / 2;
            float theta = enemyAttackSO.angleDeg / (enemyAttackSO.amount - 1);

            for(int i = 0; i < enemyAttack.amount; i++)
            { 
                var projectile = Instantiate(projectilePrefab, projectileSpawnPosition.position, Quaternion.identity);

                projectile.transform.rotation = transform.rotation;

                float curTheta = startTheta + (theta * i);
                float cos = Mathf.Cos(curTheta * Mathf.Deg2Rad);
                float sin = Mathf.Sin(curTheta * Mathf.Deg2Rad);

                projectile.GetComponent<EnemyProjectileController>().Init(transform.TransformDirection(new(cos, sin)).normalized, enemyAttackSO);
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {   
        for (int i = 0; i < enemyAttack.attackCountAtOnce; i++)
        {
            OnShot();
            yield return new WaitForSeconds(enemyAttack.delay);
        }
    }
}
