using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    private void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            EnemyController enemy = collision.transform.GetComponent<EnemyController>();
            if(enemy != null )
            {
                enemy.TakeDamage(damage);
            }
        }
        else if (collision.transform.tag == "Destroyable")
        {
            //Destroyableobject.TakeDamage || DestroyableObject.GetDestroyed
        }
        Destroy(gameObject);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
