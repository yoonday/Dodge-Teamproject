using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    [SerializeField] List<GameObject> items;

    private HealthSystem healthSystem;
    private EnemySpawner spawner;
    private Animator animator;
    private DodgeEnemyController dodgeEnemyController;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += OnDeath;

        spawner = EnemySpawner.Instance;
        animator = GetComponentInChildren<Animator>();
        dodgeEnemyController = GetComponent<DodgeEnemyController>();
    }

    void OnDeath()
    {
        dodgeEnemyController.Stop();

        int rand = Random.Range(0, items.Count);

        Instantiate(items[rand], transform.position, Quaternion.identity);

        spawner.EnemyDestroyed(dodgeEnemyController.IsBoss);

        animator.SetTrigger("isDeath");
        healthSystem.OnDeath -= OnDeath;

        StartCoroutine(SetActiveFalseCoroutine());
    }
    
    private IEnumerator SetActiveFalseCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
