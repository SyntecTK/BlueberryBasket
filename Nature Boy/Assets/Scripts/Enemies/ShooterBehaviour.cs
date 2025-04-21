using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShooterBehaviour : EnemyBase
{
    [Header("Components")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("EnemyStats")]
    [SerializeField] private float health;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float hoverSpeed = 0.5f;
    [SerializeField] private float fleeRange = 2f;

    private float shootCooldown = 1f;
    private float lastShootTime;
    private float hoverChangeTimer = 0f;
    private Vector2 hoverDirection;

    public override void Act(Transform player, Rigidbody2D rb, float moveSpeed)
    {
        Vector2 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        //Flee from player
        if (distance < fleeRange)
        {
            Vector2 moveDir = SnapTo8Directions(-toPlayer);
            rb.linearVelocity = moveDir * moveSpeed;
        }
        //Shoot at player
        else if (distance <= attackRange)
        {
            rb.linearVelocity = Vector2.zero;

            if (Time.time > lastShootTime + shootCooldown)
            {
                ShootAtPlayer(toPlayer);
                lastShootTime = Time.time;
            }
        }
        // Idle hover
        else
        {
            hoverChangeTimer -= Time.deltaTime;
            if (hoverChangeTimer <= 0)
            {
                PickNewHoverDirection();
            }

            rb.linearVelocity = hoverDirection * hoverSpeed;
        }
    }

    private void PickNewHoverDirection()
    {
        Vector2[] directions = new Vector2[]
        {
            Vector2.up,
            new Vector2(1, 1).normalized,
            Vector2.right,
            new Vector2(1, -1).normalized,
            Vector2.down,
            new Vector2(-1, -1).normalized,
            Vector2.left,
            new Vector2(-1, 1).normalized
        };

        hoverDirection = directions[Random.Range(0, directions.Length)];
        hoverChangeTimer = Random.Range(1.5f, 3f);
    }

    void ShootAtPlayer(Vector2 dir)
    {
        Vector2 shootDir = SnapTo8Directions(dir);
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().linearVelocity = shootDir * 10f;
    }
}
