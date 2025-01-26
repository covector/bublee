using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Meteor : MonoBehaviour
{
    private Rigidbody2D rb;
    private float fallSpeed;
    private float fallAngle;

    [Header("Destroy Meteor After Seconds")]
    public float lifetime = 10f; // Destroy after 10 seconds if it doesn't collide

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Destroy meteor after 'lifetime' seconds to prevent buildup
        Destroy(gameObject, lifetime);
    }

    /// <summary>
    /// Called by the spawner to set angle/speed.
    /// </summary>
    /// <param name="angle">Angle in degrees relative to straight down.</param>
    /// <param name="speed">Speed of the meteor's fall.</param>
    public void SetFallParameters(float angle, float speed)
    {
        fallAngle = angle;
        fallSpeed = speed;

        // Convert angle to radians for 2D direction
        float rad = angle * Mathf.Deg2Rad;

        // Straight down is Vector2.down => (0, -1)
        // We'll rotate that by 'angle' around Z
        // e.g. if angle = 0 => straight down
        //     if angle = 20 => 20 deg left or right
        Vector2 fallDir = Quaternion.Euler(0, 0, angle) * Vector2.down;

        // If you want to just set a velocity:
        rb.linearVelocity = fallDir * fallSpeed;

        // Optional: rotate the sprite so it faces direction of movement
        // transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    /// <summary>
    /// Detect collision with player. If the meteor hits the player, do something (e.g. damage).
    /// </summary>
    /// <param name="collision">Collision info.</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Meteor hit the player!");
            Destroy(gameObject);

            FindFirstObjectByType<DeathScreen>().ShowDeathScreen();

        }
    }
}
