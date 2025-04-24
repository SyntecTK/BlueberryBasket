using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Sprite openCage;
    [SerializeField] private GameObject baby;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            GetComponent<SpriteRenderer>().sprite = openCage;
            BabyBehaviour babyBoy = baby.transform.GetComponent<BabyBehaviour>();
            babyBoy.StartFollowing();
        }
    }
}
