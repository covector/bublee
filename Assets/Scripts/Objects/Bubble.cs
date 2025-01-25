using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float terminalVelocity = 2f; // Maximum speed of the bubble

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Clamp the velocity to the terminal velocity
        if (rb.linearVelocity.magnitude > terminalVelocity)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * terminalVelocity;
        }
    }
}
