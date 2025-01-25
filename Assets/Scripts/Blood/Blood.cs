using UnityEngine;
using UnityEngine.UI;

public class Blood : MonoBehaviour
{
    public GameObject[] bloodBalls; // Expect 8 in the array
    public void UpdateHealthUI(int currentHealth)
    {
        // Safety check
        if (bloodBalls == null || bloodBalls.Length == 0) return;
        Destroy(bloodBalls[7-currentHealth]);
    }
}
