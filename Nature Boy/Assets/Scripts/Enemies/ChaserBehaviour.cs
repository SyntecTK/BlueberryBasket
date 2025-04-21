
using UnityEngine;

public class ChaserBehaviour : EnemyBase
{
    [SerializeField] private int health;
    public override void Act(Transform player, Rigidbody2D rb, float moveSpeed)
    {
        Vector2 dir = player.position - transform.position;
        Vector2 moveDir = SnapTo8Directions(dir);
        rb.linearVelocity = moveDir * moveSpeed;
    }
}