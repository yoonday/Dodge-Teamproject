using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    private Vector2 shotDirection;
    private Rigidbody2D rigidbody;
    private EnemyAttackSO enemyAttackSO;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 shotDirection, EnemyAttackSO enemyAttackSO)
    {
        this.shotDirection = shotDirection;
        this.enemyAttackSO = enemyAttackSO;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = shotDirection * enemyAttackSO.speed;
    }
}
