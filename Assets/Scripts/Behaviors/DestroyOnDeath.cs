using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    [SerializeField] List<GameObject> items;

    private HealthSystem healthSystem;
    private EnemySpawner spawner;
    private Animator animator;
    private DodgeEnemyController dodgeEnemyController;
    private Collider2D collider;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += OnDeath;

        spawner = EnemySpawner.Instance;
        animator = GetComponentInChildren<Animator>();
        dodgeEnemyController = GetComponent<DodgeEnemyController>();
        collider = GetComponent<Collider2D>();
    }

    void OnDeath()
    {
        dodgeEnemyController.Stop();

        int rand = Random.Range(0, items.Count);

        Instantiate(items[rand], transform.position, Quaternion.identity);

        spawner.EnemyDestroyed(dodgeEnemyController.IsBoss);

        collider.enabled = false;
        animator.enabled = true;

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Death);

        StartCoroutine(SetActiveFalseCoroutine());
    }
    
    private IEnumerator SetActiveFalseCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        animator.Rebind();
        animator.enabled = false;
        gameObject.SetActive(false);
    }
}
