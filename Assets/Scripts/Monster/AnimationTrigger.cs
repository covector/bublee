using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public Animator animator;
    [ContextMenu("TriggerHurt")]
    public void TriggerHurt()
    {
        animator.ResetTrigger("Hurt");
        animator.SetTrigger("Hurt");
    }
    [ContextMenu("PlayRush")]
    public void PlayRush()
    {
        animator.SetBool("Rush", true);
    }
    [ContextMenu("StopRush")]
    public void StopRush()
    {
        animator.SetBool("Rush", false);
    }
    [ContextMenu("PlayGatherEnergy")]
    public void PlayGatherEnergy()
    {
        animator.SetBool("GatherEnergy", true);
    }
    [ContextMenu("StopGatherEnergy")]
    public void StopGatherEnergy()
    {
        animator.SetBool("GatherEnergy", false);
    }
    [ContextMenu("Death")]
    public void Death()
    {
        animator.SetTrigger("Death");
    }
}
