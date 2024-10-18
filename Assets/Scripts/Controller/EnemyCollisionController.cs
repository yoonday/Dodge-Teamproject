using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionController : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            healthSystem.ChangeHealth(-1);

            if (collision.gameObject.GetComponent<DodgeController>() == null) { Destroy(collision.gameObject); }
        }
    }
}
