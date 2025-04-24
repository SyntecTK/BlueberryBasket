using UnityEngine;

public class PlayerController : MonoBehaviour 
{
    [SerializeField] private float moveSpeed;
    [Header("Dash Stats")]
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private Vector2 dashDirection;
    private bool isDashing;
    private float dashTimer;
    private float dashCooldownTimer;

    private Vector2 input;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    Animator anim;
    private Vector2 lastMoveDirection;
    private bool facingLeft = true;

    public bool shootingActive { get; private set; } = true;
    private Shooting shootingScript;
    [SerializeField] private GameObject staffGO;
    [SerializeField] private GameObject crosshair;
    public bool meleeActive { get; private set; } = false;
    private MeleeAttack meleeScript;
    [SerializeField] private GameObject meleeGO;

    [SerializeField] private int maxHealth = 5;
    private int currentHealth;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        shootingScript = GetComponent<Shooting>();
        meleeScript = GetComponent<MeleeAttack>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update() {
        ProcessInputs();
        Animate();
        if (input.x < 0 && facingLeft || input.x > 0 && !facingLeft) {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            shootingActive = !shootingActive;
            shootingScript.enabled = !shootingScript.enabled;
            staffGO.SetActive(!staffGO.activeSelf);
            crosshair.SetActive(!crosshair.activeSelf);

            meleeActive = !meleeActive;
            meleeScript.enabled = !meleeScript.enabled;
            meleeGO.SetActive(!meleeGO.activeSelf);
        }

        HandleDash();
    }
    private void FixedUpdate()
    {
        if(!isDashing)
        {
            rb.linearVelocity = input * moveSpeed;
        }
    }

    private void HandleDash()
    {
        if (dashCooldownTimer > 0) dashCooldownTimer -= Time.deltaTime;
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0f)
        {
            dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            if (dashDirection != Vector2.zero)
            {
                isDashing = true;
                dashTimer = dashDuration;
                dashCooldownTimer = dashCooldown;
                rb.linearVelocity = dashDirection * dashSpeed;
            }
        }
    }


    private Vector2 Get8Direction(Vector2 input) {
        input.Normalize();
        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        float snappedAngle = Mathf.Round(angle / 45f) * 45f;
        float radians = snappedAngle * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
    }

    void ProcessInputs() {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0)) {
            lastMoveDirection = input;
        }

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        input.Normalize();

        if (input == Vector2.zero) {
            moveDirection = Vector2.zero;
        }

        moveDirection = Get8Direction(input);
    }

    void Animate() {
        anim.SetFloat("MoveX", input.x);
        anim.SetFloat("MoveY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastMoveX", lastMoveDirection.x);
        anim.SetFloat("LastMoveY", lastMoveDirection.y);
    }

    void Flip() {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingLeft = !facingLeft;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GameManager.Instance.UpdatePlayerLife(currentHealth);
    }
    public void Heal(int value)
    {
        currentHealth += value;
        GameManager.Instance.UpdatePlayerLife(currentHealth);
    }
}
