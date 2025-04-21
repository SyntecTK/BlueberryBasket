using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private MeleeHitbox hitbox;
    private MeleeHitbox hitboxScript;
    [SerializeField] private float attackCooldown = 1f;
    private float attackTimer = 0f;
    private bool canAttack = true;

    void Update()
    {
        CheckMeleeTimer();
    }

    private void OnAttack(InputValue inputValue)
    {
        if (hitbox.enemyIsClose && canAttack)
        {
            if (hitbox.enemies.Count > 0)
            {
                List<GameObject> enemiesCopy = new List<GameObject>(hitbox.enemies);
                foreach (var enemy in enemiesCopy)
                {
                    hitbox.enemies.Remove(enemy);
                    Destroy(enemy);
                }
            }
        }
    }

    private void CheckMeleeTimer()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            canAttack = true;
        }
    }
}
