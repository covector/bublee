using TMPro;
using UnityEngine;

public class FloorAppleDie : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Apple"))
        {
            Debug.Log("Apple dropped on the floor, YOU DIE !!! ");


            FindFirstObjectByType<DeathScreen>().ShowDeathScreen();
            //GetComponent<Canvas>().enabled = true;

            // Destroy the current apple GameObject
            //Destroy(gameObject);

        }

    }

    void Update()
    {
        //update the score text if score > 0

        scoreText.text = score > 0 ? score.ToString() : "";

    }





}
