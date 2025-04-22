using System.Runtime.CompilerServices;
using UnityEngine;

public class DestroyAnimation : MonoBehaviour
{
    private EnemyController enemy;
    private Animator animator;

    private void Awake()
    {
        enemy = GetComponent<EnemyController>();
        animator = GetComponent<Animator>();

        if(enemy != null && animator != null)
        {
            string animation = "";
            switch(enemy.Type)
            {
                case EnemyController.EnemyType.Human:
                    animation = "NormalExplosion";
                    break;

                case EnemyController.EnemyType.Robot:
                    animation = "BloodExplosion";
                    break;

                case EnemyController.EnemyType.Building:
                    animation = "NormalExplosion";
                    break;
            }
            animator.Play(animation);
        }
    }
    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}
