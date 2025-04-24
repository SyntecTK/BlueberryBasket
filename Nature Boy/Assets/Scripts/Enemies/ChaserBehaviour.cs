
using UnityEngine;

public class ChaserBehaviour : EnemyBase
{
    public enum PatrolAxis
    {
        Horizontal,
        Vertical
    }

    [SerializeField] private int health;
    [SerializeField] private float chaseRange = 2f;

    [Header("Patrol Settings")]
    [SerializeField] private float patrolSpeed = 1f;
    [SerializeField] private float patrolDuration = 2f;
    [SerializeField] private PatrolAxis patrolAxis = PatrolAxis.Horizontal;

    private float patrolTimer;
    private int patrolDirection = 1; 

    public override void Act(Transform player, Rigidbody2D rb, float moveSpeed)
    {
        Vector2 dir = player.position - transform.position;
        Vector2 moveDir = SnapTo8Directions(dir);
        Vector2 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        if(distance < chaseRange)
        {
            rb.linearVelocity = moveDir * moveSpeed;
            transform.localScale = new Vector3(dir.x < 0 ? -1 : 1, 1, 1);
            Debug.Log("Chasing");
        }
        else
        {
            Patrol(rb);
        }
    }

    private void Patrol(Rigidbody2D rb)
    {
        patrolTimer += Time.deltaTime;

        Vector2 moveDir = Vector2.zero;
        if (patrolAxis == PatrolAxis.Horizontal)
        {
            moveDir = Vector2.right * patrolDirection;
        }
        else if (patrolAxis == PatrolAxis.Vertical)
        {
            moveDir = Vector2.up * patrolDirection;
        }

        rb.linearVelocity = moveDir * patrolSpeed;

        if (patrolTimer >= patrolDuration)
        {
            patrolTimer = 0f;
            patrolDirection *= -1;
            transform.localScale = new Vector3(patrolDirection < 0 ? -1 : 1, 1, 1);
        }
    }
}