using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    [Header("Attack Settings")]
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    public Transform player;
    public Animator animator;
    public GameObject attackHitbox; // The hitbox that deals damage

    private bool isAttacking = false;
    private float nextAttackTime = 0f;

    void Update() {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackRange && Time.time >= nextAttackTime) {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack() {
        isAttacking = true;
        nextAttackTime = Time.time + attackCooldown;

        // Play attack animation
        animator.SetTrigger("DoAttack");

        // Optional: wait for a moment before enabling hitbox (to sync with animation)
        yield return new WaitForSeconds(0.2f); // adjust to match animation timing

        // Enable hitbox
        attackHitbox.SetActive(true);

        // Keep it active for a short duration
        yield return new WaitForSeconds(0.2f); // adjust as needed
        attackHitbox.SetActive(false);

        isAttacking = false;
    }
}
