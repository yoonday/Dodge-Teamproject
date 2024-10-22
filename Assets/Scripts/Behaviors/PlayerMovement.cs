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

    private Vector2 movementDirection = Vector2.zero; // ?§Î•ò Î∞©Ï? Ï∞®Ïõê

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
        direction = direction * currentSpeed; // SpaceshipSpeed?êÏÑú ?ÑÏù¥???ÅÏö© Í∞íÏúºÎ°?Î∞îÍøà
        movementRigidbody.velocity = direction;
    }
}
