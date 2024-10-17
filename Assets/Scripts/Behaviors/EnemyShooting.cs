using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private GameObject projectile;

    private DodgeController controller;
    private EnemyAttackSO enemyAttack;


    private void Awake()
    {
        controller = GetComponent<DodgeController>();
        enemyAttack = (controller as DodgeEnemyController).EnemyAttack;
    }

    void Start()
    {
        controller.OnAttackEvent += OnShoot;
    }

    private void OnShoot()
    {
        if (enemyAttack.angle == EnemyAttackSO.Angle.STRAIGHT)
        {
            Instantiate(projectile, projectileSpawnPosition.position, Quaternion.identity);
        }
        else
        {
            Instantiate(projectile, projectileSpawnPosition.position, Quaternion.identity);
        }
    }

}
