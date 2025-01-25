using UnityEngine;
using System.Collections;

public class MonsterBehavior : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 2f;          // Speed at which the monster walks
    public float chargeTime = 1f;         // Time spent "charging" in place
    public float walkTime = 0.5f;         // Time spent walking
    public float dashSpeed = 8f;          // Speed at which the monster dashes
    public float dashDistance = 5f;       // How far the monster travels when dashing

    [Header("Health Settings")]
    public int maxHealth = 8;              // Maximum health of the monster
    private int currentHealth;             // Current health

    private Transform player;
    private Vector2 dashDirection;
    private bool isDashing;
    private float traveledDistance;

    // Possible states for the monster
    private enum MonsterState
    {
        Walking,
        Charging,
        Dashing
    }

    private MonsterState currentState;

    // Optional: Reference to a health bar UI or effects
    // public HealthBar healthBar;

    private void Start()
    {
        // Initialize health
        currentHealth = maxHealth;
        // Optionally initialize health bar
        // if (healthBar != null)
        // {
        //     healthBar.SetMaxHealth(maxHealth);
        // }

        // Assume your Player has tag "Player" or reference the Transform however you like
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Ensure the player has the tag 'Player'.");
        }

        // Begin in the Walking state
        currentState = MonsterState.Walking;
        // Start the state machine as a coroutine
        StartCoroutine(MonsterStateMachine());
    }

    private IEnumerator MonsterStateMachine()
    {
        while (true)
        {
            switch (currentState)
            {
                case MonsterState.Walking:
                    yield return StartCoroutine(Walking());
                    break;

                case MonsterState.Charging:
                    yield return StartCoroutine(Charging());
                    break;

                case MonsterState.Dashing:
                    yield return StartCoroutine(Dashing());
                    break;
            }
        }
    }

    private IEnumerator Walking()
    {
        float timer = 0f;

        // Walk for walkTime seconds
        while (timer < walkTime)
        {
            timer += Time.deltaTime;

            // Move horizontally toward the player
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            directionToPlayer.y = 0f;  // Lock the monster to ground on y-axis

            transform.Translate(directionToPlayer * walkSpeed * Time.deltaTime, Space.World);

            // Face the player if you want sprite flip, etc.
            FlipSpriteIfNeeded(directionToPlayer);

            yield return null;
        }
        // Transition to Charging
        currentState = MonsterState.Charging;
    }

    private IEnumerator Charging()
    {
        float timer = 0f;

        // Charging is basically "locking in" the direction, so let's store it once:
        dashDirection = (player.position - transform.position).normalized;
        dashDirection.y = 0f;

        // Ensure dash direction is either left or right
        dashDirection = dashDirection.x >= 0 ? Vector2.right : Vector2.left;

        // Stand still for chargeTime seconds
        while (timer < chargeTime)
        {
            timer += Time.deltaTime;

            // Optionally play a charging animation, show VFX, etc.

            yield return null;
        }

        // Transition to Dashing
        currentState = MonsterState.Dashing;
    }

    private IEnumerator Dashing()
    {
        isDashing = true;
        traveledDistance = 0f;

        // Dash until we've traveled the designated distance
        while (traveledDistance < dashDistance)
        {
            float step = dashSpeed * Time.deltaTime;
            transform.Translate(dashDirection * step, Space.World);
            traveledDistance += step;

            // The sprite might need flipping to face dash direction
            FlipSpriteIfNeeded(dashDirection);

            yield return null;
        }

        isDashing = false;

        // After dash, return to Walking
        currentState = MonsterState.Walking;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {   
        Debug.Log("Monster collided with " + collision);
        // Handle collision with Player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Monster collided with player during dash!");
            // Call the player's death method
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                Debug.Log("PLAYER DIE");
            }
            else
            {
                Debug.LogWarning("PlayerController script not found on Player object.");
            }
        }

        // Handle collision with Bubble
        //if (collision.gameObject.CompareTag("Bubble"))
        //{
        //    Debug.Log("Monster hit by bubble!");
        //    TakeDamage(1);

        //    // Destroy the bubble after collision
        //    Destroy(collision.gameObject);
        //}
    }

    /// <summary>
    /// Reduces the monster's health and checks for death.
    /// </summary>
    /// <param name="damage">Amount of damage to take.</param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Monster took {damage} damage. Current Health: {currentHealth}");

        // Optionally update health bar
        // if (healthBar != null)
        // {
        //     healthBar.SetHealth(currentHealth);
        // }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Handles the monster's death.
    /// </summary>
    private void Die()
    {
        Debug.Log("Monster has died!");

        // Optionally play death animation or effects here

        // Destroy the monster game object
        Destroy(gameObject);
    }

    /// <summary>
    /// Flips the monster's sprite based on movement direction.
    /// Assumes the sprite faces right by default.
    /// </summary>
    /// <param name="direction">Direction vector.</param>
    private void FlipSpriteIfNeeded(Vector2 direction)
    {
        if (direction.x < 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x,
                                               transform.localScale.y,
                                               transform.localScale.z);
        }
        else if (direction.x > 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x,
                                               transform.localScale.y,
                                               transform.localScale.z);
        }
    }
}
