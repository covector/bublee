using System.Collections;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public float walkSpeed = 2f; // Speed of walking
    public float dashSpeed = 10f; // Speed of dashing
    public float walkSteps = 3f; // Number of steps to walk
    public float chargeDuration = 1.5f; // Charging time before dash
    public float cooldownDuration = 0.5f; // Cooldown after dashing
    public Transform player; // Reference to the player's transform

    private Vector3 targetDirection; // Direction towards the player
    private bool isDashing = false;

    private void Start()
    {
        StartCoroutine(MonsterCycle());
    }

    private IEnumerator MonsterCycle()
    {
        while (true)
        {
            // Step 1: Walk 3 steps
            yield return StartCoroutine(WalkSteps());

            // Step 2: Charge for 1.5 seconds
            yield return StartCoroutine(Charge());

            // Step 3: Dash towards the player
            yield return StartCoroutine(Dash());

            // Step 4: Cooldown for 0.5 seconds
            yield return StartCoroutine(Cooldown());

            // Step 5: Adjust head direction towards the player
            AdjustHeadDirection();
        }
    }

    private IEnumerator WalkSteps()
    {
        float walkedDistance = 0f;

        while (walkedDistance < walkSteps)
        {
            Vector3 walkDirection = transform.right; // Assuming the monster walks along its right direction
            transform.position += walkDirection * walkSpeed * Time.deltaTime;
            walkedDistance += walkSpeed * Time.deltaTime;

            yield return null; // Wait for the next frame
        }
    }

    private IEnumerator Charge()
    {
        Debug.Log("Charging...");
        float elapsed = 0f;

        while (elapsed < chargeDuration)
        {
            elapsed += Time.deltaTime;
            // You can add a charge effect here (e.g., animations or VFX)
            yield return null;
        }
    }

    private IEnumerator Dash()
    {
        Debug.Log("Dashing!");
        isDashing = true;

        targetDirection = (player.position - transform.position).normalized; // Calculate direction towards the player
        float dashTime = 0.5f; // Set dash duration
        float elapsed = 0f;

        while (elapsed < dashTime)
        {
            transform.position += targetDirection * dashSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }

    private IEnumerator Cooldown()
    {
        Debug.Log("Cooldown...");
        yield return new WaitForSeconds(cooldownDuration);
    }

    private void AdjustHeadDirection()
    {
        // Rotate the monster's head towards the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}