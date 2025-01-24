using UnityEngine;

public class Shooting : MonoBehaviour
{
    public LineRenderer lineRenderer; // Attach a LineRenderer component
    public Transform firePoint; // The starting point of the line
    public GameObject projectilePrefab; // The projectile to fire
    public float projectileSpeed = 10f; // Speed of the projectile

    void Update()
    {
        AimWithLine();
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            FireProjectile();
        }
    }

    void AimWithLine()
    {
        // Get mouse position in world space for 2D
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure z is zero for 2D games

        // Draw the line from the firePoint to the mouse
        lineRenderer.SetPosition(0, firePoint.position); // Start of the line
        lineRenderer.SetPosition(1, mousePosition); // End of the line
    }

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
            rb.linearVelocity = direction * projectileSpeed;
        }
    }
}