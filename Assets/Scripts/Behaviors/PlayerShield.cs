using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.CompareTag("Enemy"))
        {
            Enemy.gameObject.GetComponent<HealthSystem>()?.ChangeHealth(-int.MaxValue);
        }
    }
}
