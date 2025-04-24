using UnityEngine;

public class AttackHitbox : MonoBehaviour {
    public int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag == "Player") {
            Debug.Log("Player hit!");
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
        }

        /*void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            // Replace with your player health logic
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
            Debug.Log("Player hit!");
            // other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }*/
    }
}