using System.Runtime.CompilerServices;
using UnityEngine;

public class DestroyAnimation : MonoBehaviour
{
    private EnemyController enemy;
    private Animator animator;
    private AudioSource audioSource;

    public void Initialize(EnemyController enemy)
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

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
            animator.Play(animation);
            audioSource.Play();
        }
    }
    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }

    public void DoubleSize()
    {
        transform.localScale = new Vector3(2f, transform.localScale.y * 2, transform.localScale.z * 2);
    }
}
