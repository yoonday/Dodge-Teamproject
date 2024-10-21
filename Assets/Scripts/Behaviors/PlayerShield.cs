using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.CompareTag("Enemy"))
        {
            var healthSystem = Enemy.gameObject.GetComponent<HealthSystem>();
            var enemyController = Enemy.gameObject.GetComponent<DodgeEnemyController>();

            if (healthSystem != null && enemyController.IsBoss == false)
            {
                healthSystem.ChangeHealth(-int.MaxValue);
            }
            else
            {
                Enemy.gameObject.SetActive(false);
            }
        }
    }
}
