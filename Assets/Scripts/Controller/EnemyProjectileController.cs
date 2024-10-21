using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    private Vector2 shotDirection;
    private Rigidbody2D rigidbody;
    private EnemyAttackSO enemyAttackSO;

    private float screenBottomMax;

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

        if (transform.position.y < screenBottomMax)
        {
            gameObject.SetActive(false);
        }
    }

    private void SetRange()
    {
        Camera mainCamera = Camera.main;

        float cameraHeight = mainCamera.orthographicSize;

        screenBottomMax = mainCamera.transform.position.y - cameraHeight; // 화면 맨 아래 좌표.
    }
}
