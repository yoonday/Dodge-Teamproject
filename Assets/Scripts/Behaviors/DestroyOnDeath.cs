using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += OnDeath;
    }

    void OnDeath()
    {
        /*
         *      죽었을 때 아이템 생성되게.
         *      적이 죽을 때 이펙트 반영?
         *      
         *      
         *      아이템이 만들어지는 경우의 수
         *      적이 죽었을 때
         *      
         *      ItemSpawner -> 아이템 생성 (랜덤)
         *      
         *      transform.position
         */


        Destroy(gameObject);
    }
}
