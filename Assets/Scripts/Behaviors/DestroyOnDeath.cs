using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    [SerializeField] List<GameObject> items;

    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += OnDeath;
    }

    void OnDeath()
    {
        int rand = Random.Range(0, items.Count);

        Instantiate(items[rand], transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
