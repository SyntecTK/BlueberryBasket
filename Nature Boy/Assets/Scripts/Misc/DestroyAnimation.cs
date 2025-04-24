using System.Runtime.CompilerServices;
using UnityEngine;

public class DestroyAnimation : MonoBehaviour
{
    private EnemyController enemy;
    private Animator animator;

    public void Initialize(EnemyController enemy)
    {
        animator = GetComponent<Animator>();
        Debug.Log($"Animator: {animator}");
        Debug.Log($"Enemy: {enemy}");

        if(enemy != null && animator != null)
        {
            string animation = "";
            switch(enemy.Type)
            {
                case EnemyController.EnemyType.Human:
                    animation = "BloodExplosion";
                    break;

                case EnemyController.EnemyType.Robot:
                    animation = "NormalExplosion";
                    break;

                case EnemyController.EnemyType.Building:
                    animation = "NormalExplosion";
                    break;
            }
            Debug.Log($"Playing animation {animation}");
            animator.Play(animation);
        }
    }
    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}
