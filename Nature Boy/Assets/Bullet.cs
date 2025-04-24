using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private GameObject originObject;

    private void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    public void SetOriginObject(GameObject origin)
    {
        originObject = origin;   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            EnemyController enemy = collision.transform.GetComponent<EnemyController>();
            if(enemy != null && collision.gameObject != originObject)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (collision.transform.tag == "Destroyable")
        {
            Destructable destructable = collision.transform.GetComponent<Destructable>();
            destructable.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.transform.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if(player != null && collision.gameObject != originObject)
            {
                player.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
