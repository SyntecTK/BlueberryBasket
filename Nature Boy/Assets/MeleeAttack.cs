using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttack : MonoBehaviour {
    [SerializeField] private MeleeHitbox hitbox;
    private MeleeHitbox hitboxScript;
    [SerializeField] private float attackCooldown = 1f;
    private float attackTimer = 0f;
    private bool canAttack = true;

    //for Attack Combo if wanted
    int comboStep = 0;
    float lastAttackTime;
    public float comboResetTime = 1.0f;
    private Animator anim;


    void Start() {
        anim = GetComponent<Animator>();
    }

    void Update() {
        CheckMeleeTimer();



    }

    private void OnAttack(InputValue inputValue) {
        if (!this.enabled)
        {
            return;
        }
        anim.SetTrigger("MeleeAttack");
        //for Attack Combo if wanted
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{ // Or your attack key
        //    if (Time.time - lastAttackTime > comboResetTime)
        //    {
        //        comboStep = 1; // Reset combo
        //    }
        //    else
        //    {
        //        comboStep++;
        //    }

        //    comboStep = Mathf.Clamp(comboStep, 1, 3);
        //    anim.SetInteger("ComboStep", comboStep);
        //    anim.SetTrigger("AttackPressed");

        //    lastAttackTime = Time.time;
        //}

        // Reset to 0 after combo ends (optional)
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        //{
        //    anim.SetInteger("ComboStep", 0);
        //}
        if (hitbox.enemyIsClose && canAttack) {
            if (hitbox.enemies.Count > 0) {
                List<GameObject> enemiesCopy = new List<GameObject>(hitbox.enemies);
                foreach (var enemy in enemiesCopy) {
                    if (enemy.gameObject.tag == "Enemy")
                    {
                        hitbox.enemies.Remove(enemy);
                        enemy.gameObject.GetComponent<EnemyController>().TakeDamage(3);
                    } else if (enemy.gameObject.tag == "Destroyable")
                    {
                        enemy.gameObject.GetComponent<Destructable>().TakeDamage(1);
                    }
                }
            }
        }
    }

    private void CheckMeleeTimer() {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown) {
            canAttack = true;
        }
    }
}
