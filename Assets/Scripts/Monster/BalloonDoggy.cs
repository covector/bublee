using UnityEditor;
using UnityEngine;

public class BalloonDoggy : MonoBehaviour
{
    private MonsterBehavior monsterBehavior;
    public Animator animator;
    public Blood blood;

    private void Start()
    {
        // Get the parent script
        monsterBehavior = GetComponentInParent<MonsterBehavior>();
        if (monsterBehavior == null)
        {
            Debug.LogError("MonsterBehavior not found on parent!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only handle collisions with bubbles
        if (collision.CompareTag("Bubble"))
        {
            Debug.Log("BalloonDoggy (weak spot) hit by bubble!");

            // If we found the parent script, call TakeDamage on the monster
            if (monsterBehavior != null)
            {
                monsterBehavior.TakeDamage(1);
                animator.ResetTrigger("Hurt");
                animator.SetTrigger("Hurt");
            }

            // Destroy the bubble so it doesn't deal damage repeatedly
            Destroy(collision.gameObject);
        }
    }
}