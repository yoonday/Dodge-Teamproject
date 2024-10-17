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
        // 같은 게임오브젝트의 TopDownController, Rigidbody를 가져올 것 
        movementController = GetComponent<DodgeController>();
        movementRigidbody = GetComponent<Rigidbody2D>();

        enemySpeed = (movementController as DodgeEnemyController).EnemySpeed;
    }

    private void Start()
    {
        // OnMoveEvent에 Move를 호출하라고 등록함
        movementController.OnMoveEvent += Move;
    }

    private void FixedUpdate()
    {
        // 물리 업데이트에서 움직임 적용
        ApplyMovement(movementDirection);
    }

    private void Move(Vector2 destination)
    {
        // 이동방향만 정해두고 실제로 움직이지는 않음.
        // 움직이는 것은 물리 업데이트에서 진행(rigidbody가 물리니까)
        movementDirection = destination;
    }

    private void ApplyMovement(Vector2 destination)
    {
        if (destination == Vector2.zero) { return; }

        transform.position = Vector2.Lerp(transform.position, destination, enemySpeed * Time.deltaTime);
    }
}
