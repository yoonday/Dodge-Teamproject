using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // [SerializeField] float spaceshipSpeed = 5;

    private DodgeController controller;
    private Rigidbody2D movementRigidbody;
    private PlayerStatHandler playerStatHandler;

    private Vector2 movementDirection = Vector2.zero; // ?�류 방�? 차원

    private void Awake()
    {
        controller = GetComponent<DodgeController>();
        movementRigidbody = GetComponent<Rigidbody2D>();
        playerStatHandler = GetComponent<PlayerStatHandler>();
    }

    void Start()
    {
        controller.OnMoveEvent += Move;
    }

    private void Move(Vector2 direction)
    {
        movementDirection = direction;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyMovement(movementDirection);
    }

    private void ApplyMovement(Vector2 direction)
    {
        float currentSpeed = playerStatHandler.CurrentStat.speed;
        direction = direction * currentSpeed; // SpaceshipSpeed?�서 ?�이???�용 값으�?바꿈
        movementRigidbody.velocity = direction;
    }
}
