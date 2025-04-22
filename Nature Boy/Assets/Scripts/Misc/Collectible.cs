using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Collectible : MonoBehaviour
{
    [SerializeField] private float launchForce = 3f;
    [SerializeField] private float slowdownTime = 0.5f;
    [SerializeField] private float hoverSpeed = 3f;
    [SerializeField] private float hoverHeight = 0.1f;

    private Rigidbody2D rb;
    private Vector3 basePosition;
    private float lifetime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Random.insideUnitCircle.normalized * launchForce;

        basePosition = transform.position;
    }

    void Update()
    {
        lifetime += Time.deltaTime;

        if (lifetime >= slowdownTime)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.deltaTime * 5f);
        }

        float hoverY = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, basePosition.y + hoverY, pos.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            GameManager.Instance?.CollectiblePickedUp();
            Destroy(gameObject);
        }
    }
}
