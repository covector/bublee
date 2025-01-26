using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Line Renderer Settings")]
    public LineRenderer lineRenderer; // Attach a LineRenderer component
    public Transform firePoint; // The starting point of the line

    [Header("Projectile Settings")]
    public GameObject projectilePrefab; // The projectile to fire
    //public float projectileSpeed = 10f; // Speed of the projectile
    public float projectileForce = 3f; // Force applied to the projectile

    [Header("Fire Rate Settings")]
    [Tooltip("Number of shots allowed per second.")]
    public float fireRate = 2f; // Shots per second
    private float fireCooldown = 0f; // Time until next shot can be fired

    void Update()
    {
        AimWithLine();
        HandleShooting();
    }

    /// <summary>
    /// Handles aiming the line renderer towards the mouse position.
    /// </summary>
    void AimWithLine()
    {
        // Get mouse position in world space for 2D
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure z is zero for 2D games

        // Draw the line from the firePoint to the mouse
        lineRenderer.SetPosition(0, firePoint.position); // Start of the line
        lineRenderer.SetPosition(1, mousePosition); // End of the line
    }

    /// <summary>
    /// Handles shooting projectiles with fire rate limiting.
    /// </summary>
    void HandleShooting()
    {
        // Update the cooldown timer
        if (fireCooldown > 0f)
        {
            fireCooldown -= Time.deltaTime;
        }

        // Check for shooting input and cooldown
        if (Input.GetMouseButtonDown(0) && fireCooldown <= 0f && !isPaused)
        {
            FireProjectile();
            fireCooldown = 1f / fireRate; // Reset cooldown
        }
    }

    /// <summary>
    /// Instantiates and launches the projectile towards the mouse position.
    /// </summary>
    void FireProjectile()
    {
        // Get mouse position in world space for 2D
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure z is zero for 2D games

        // Calculate direction
        Vector3 direction = (mousePosition - firePoint.position).normalized;

        // Instantiate and launch the projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>(); // Use Rigidbody2D for 2D physics
        if (rb != null)
        {
            //rb.linearVelocity = direction * projectileSpeed;
            rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);
        }
    }

    [Header("Pause Settings")]
    public bool isPaused = false; // To control pausing shooting, if needed

    // Optional: Methods to pause/unpause shooting
    public void PauseShooting()
    {
        isPaused = true;
    }

    public void UnpauseShooting()
    {
        isPaused = false;
    }
}
