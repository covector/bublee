using UnityEngine;

public class Level3PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public Animator playeranim;
    public Controls controlmode;

    private float moveX;
    private float moveY;

    public bool isPaused = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (controlmode == Controls.mobile)
        {
            UIManager.instance.EnableMobileControls();
        }
    }

    private void Update()
    {
        if (!isPaused)
        {
            // Optional: rotation based on mouse position if needed
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 lookDirection = mousePosition - transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        }

        // Set run animation (always running if moving, you can refine this logic)
        SetAnimations();

        // Flip sprite horizontally based on moveX
        if (moveX != 0)
        {
            FlipSprite(moveX);
        }
    }

    public void SetAnimations()
    {
        // Currently forces run animation = true always
        // Adjust your logic to check if moveX or moveY != 0 if you want idle vs. run
        playeranim.SetBool("run", true);
    }

    private void FlipSprite(float direction)
    {
        if (direction > 0)
        {
            // Moving right, flip sprite to the right
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction < 0)
        {
            // Moving left, flip sprite to the left
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate()
    {
        if (!isPaused)
        {
            if (controlmode == Controls.pc)
            {
                // Use WASD or Arrow Keys: Unity�s default Horizontal is AD/??, Vertical is WS/??
                moveX = Input.GetAxis("Horizontal");
                moveY = Input.GetAxis("Vertical");
            }

            // Move in both X and Y directions
            rb.linearVelocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);
        }
        else
        {
            // If paused, keep velocity at 0
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If collide with killzone, trigger death
        if (collision.gameObject.CompareTag("killzone"))
        {
            GameManager.instance.Death();
        }

        // If collide with meteor, also trigger death
        if (collision.gameObject.CompareTag("meteor"))
        {
            // Make sure your meteor is tagged "meteor" in the Inspector
            Debug.Log("Player collided with a meteor!");
            GameManager.instance.Death();
        }
    }

    // Called by UI buttons for mobile controls
    public void MobileMove(float value)
    {
        moveX = value;
    }
}
