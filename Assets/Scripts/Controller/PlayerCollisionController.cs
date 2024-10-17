using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisiontroller : MonoBehaviour
{
    
    [SerializeField] private HealthSystem healthSystem; // HealthSystem 참조


    void OnCollisionEnter2D(Collision2D collision) // 총알
    {
        // 적의 화살에 맞았을 경우
        if (collision.gameObject.CompareTag("Enemy"))
        {
            healthSystem.ChangeHealth(-1);
            Destroy(collision.gameObject); // 총알 파괴
        }
    }


}
