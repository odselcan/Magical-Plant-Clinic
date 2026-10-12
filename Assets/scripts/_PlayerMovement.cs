using UnityEngine;

public class _PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 moveInput;
    private Vector2 screenBounds;
    private float playerHalfWidth;
    private float xPosLastFrame;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        if (spriteRenderer != null)
        {
            playerHalfWidth = spriteRenderer.bounds.extents.x;
        }

        if (Camera.main != null)
        {
            screenBounds = Camera.main.ScreenToWorldPoint(
                new Vector2(Screen.width, Screen.height)
            );
        }

        xPosLastFrame = transform.position.x;
    }

    void Update()
    {
        // PC klavye kontrolü
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 keyboardInput =
            new Vector2(horizontal, vertical).normalized;

        // Mobil joystick kontrolü
        Vector2 joystickInput =
            MobileJoystick.InputDirection;

        // Joystick kullanılıyorsa joystick'i,
        // kullanılmıyorsa klavyeyi kullan
        if (joystickInput.sqrMagnitude > 0.01f)
        {
            moveInput = joystickInput;
        }
        else
        {
            moveInput = keyboardInput;
        }

        // Animasyon
        bool isMoving =
            moveInput.sqrMagnitude > 0.01f;

        if (_animator != null)
        {
            _animator.SetBool(
                "isRunning",
                isMoving
            );
        }

        FlipCharacterX();
    }

    void FixedUpdate()
    {
        rb.linearVelocity =
            moveInput * speed;

        Clamped();
    }

    private void Clamped()
    {
        float clampedX = Mathf.Clamp(
            rb.position.x,
            -screenBounds.x + playerHalfWidth,
            screenBounds.x - playerHalfWidth
        );

        float clampedY = Mathf.Clamp(
            rb.position.y,
            -screenBounds.y,
            screenBounds.y
        );

        rb.position =
            new Vector2(clampedX, clampedY);
    }

    private void FlipCharacterX()
    {
        if (transform.position.x >
            xPosLastFrame + 0.001f)
        {
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x <
                 xPosLastFrame - 0.001f)
        {
            spriteRenderer.flipX = true;
        }

        xPosLastFrame =
            transform.position.x;
    }
}