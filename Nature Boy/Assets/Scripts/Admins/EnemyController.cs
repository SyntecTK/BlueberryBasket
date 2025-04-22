using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyBase behaviour;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private GameObject collectiblePrefab;
    [SerializeField] private GameObject explosionPrefab;

    private int currentHealth;
    private Rigidbody2D rb;
    private Transform player;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        behaviour?.Act(player, rb, moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            TriggerDeath();
        }
    }

    private void TriggerDeath()
    {
        if(collectiblePrefab != null)
        {
            Instantiate(collectiblePrefab, transform.position, Quaternion.identity);

            //Ranged Enemies explode
            if(GetComponent<ShooterBehaviour>() != null)
            {
                Instantiate(explosionPrefab, GetComponent<SpriteRenderer>().transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
}
