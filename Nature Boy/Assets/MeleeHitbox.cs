using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class MeleeHitbox : MonoBehaviour
{
    public bool enemyIsClose { get; private set; } = false;
    public List<GameObject> enemies { get; private set; }

    private void Awake()
    {
        enemies = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            enemyIsClose = true;
            enemies.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            enemyIsClose = false;
            if (enemies.Contains(collision.gameObject))
            {
                enemies.Remove(collision.gameObject);
            }
        }
    }
}
