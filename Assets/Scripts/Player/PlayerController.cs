using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Animator")]
    [SerializeField] private PlayerAnimatorController animatorController;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float airControlMultiplier = 0.6f;
    private bool isJumping;

    [Header("GroundDetection")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Jump Tuning")]
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.15f;

    private float coyoteTimeCounter = 0f;
    private float jumpBufferCounter = 0f;

    private Rigidbody2D rb;
    private InputSystem_Actions inputActions;

    private float moveInput;
    private bool jumpPressed;

    private SpriteRenderer spriteRenderer;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputSystem_Actions();

        if (Accelerometer.current != null)
            InputSystem.EnableDevice(Accelerometer.current);

        inputActions.Player.Jump.performed += ctx => jumpPressed = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable() => inputActions.Enable();
    void OnDisable() => inputActions.Disable();

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        Vector2 moveVector = inputActions.Player.Move.ReadValue<Vector2>();
        moveInput = moveVector.x;
#elif UNITY_ANDROID
    if (Accelerometer.current != null)
    {
        Vector3 accel = Accelerometer.current.acceleration.ReadValue();
        float tilt = Mathf.Clamp(accel.x * 3f, -3f, 3f);
        moveInput = Mathf.Lerp(moveInput, tilt, Time.deltaTime * 5f);
    }
#endif

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            isJumping = false;
        }
        else
            coyoteTimeCounter -= Time.deltaTime;

        if (jumpPressed)
        {
            jumpBufferCounter = jumpBufferTime;
            jumpPressed = false;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        animatorController.SetGrounded(isGrounded);
        animatorController.SetJumping(!isGrounded);
    }

    void FixedUpdate()
    {
        CheckGround();
        if (!isGrounded)
            HandleMovement();
        HandleJump();
    }

    void HandleMovement()
    {
        float effectiveSpeed = moveSpeed;
        if (!isGrounded)
            effectiveSpeed *= airControlMultiplier;

        rb.linearVelocity = new Vector2(moveInput * effectiveSpeed, rb.linearVelocity.y);

        if (moveInput < 0)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;
    }

    void HandleJump()
    {
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            isJumping = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            jumpBufferCounter = 0f;
        }
    }

    void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DeathZone"))
        {
#if UNITY_ANDROID
            Handheld.Vibrate();
#endif
        }
    }
}
