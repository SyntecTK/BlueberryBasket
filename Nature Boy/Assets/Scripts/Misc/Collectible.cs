using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Collectible : MonoBehaviour
{
    public float launchForce = 3f;
    public float slowdownTime = 0.5f;
    public float hoverSpeed = 3f;
    public float hoverHeight = 0.1f;

    private Rigidbody2D rb;
    private Vector3 basePosition;
    private float lifetime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        // Launch outward
        rb.linearVelocity = Random.insideUnitCircle.normalized * launchForce;

        basePosition = transform.position;
    }

    void Update()
    {
        lifetime += Time.deltaTime;

        // Gradually slow horizontal movement
        if (lifetime >= slowdownTime)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, Time.deltaTime * 5f);
        }

        // Soft idle hover (fake vertical bounce)
        float hoverY = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, basePosition.y + hoverY, pos.z);
    }
}
