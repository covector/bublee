using UnityEngine;

public class SpeedBlend : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocityX));
        //Debug.Log(rb.linearVelocityX);
    }
}
