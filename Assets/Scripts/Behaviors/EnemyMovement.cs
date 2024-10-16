using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private DodgeController movementController;
    private Rigidbody2D movementRigidbody;

    private Vector2 movementDirection = Vector2.zero;

    private float enemySpeed;

    private void Awake()
    {
        // ���� ���ӿ�����Ʈ�� TopDownController, Rigidbody�� ������ �� 
        movementController = GetComponent<DodgeController>();
        movementRigidbody = GetComponent<Rigidbody2D>();

        enemySpeed = (movementController as DodgeEnemyController).EnemySpeed;
    }

    private void Start()
    {
        // OnMoveEvent�� Move�� ȣ���϶�� �����
        movementController.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        // ���� ������Ʈ���� ������ ����
        ApplyMovement(movementDirection);
    }

    private void Move(Vector2 destination)
    {
        // �̵����⸸ ���صΰ� ������ ���������� ����.
        // �����̴� ���� ���� ������Ʈ���� ����(rigidbody�� �����ϱ�)
        movementDirection = destination;
    }

    private void ApplyMovement(Vector2 destination)
    {
        if (destination == Vector2.zero) { return; }

        transform.position = Vector2.Lerp(transform.position, destination, enemySpeed * Time.deltaTime);
    }
}
