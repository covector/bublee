using UnityEngine;
using UnityEngine.SceneManagement;

    public class BubbleAppleBasket : MonoBehaviour
{
    public int score = 0;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        //if (collision.gameObject.CompareTag("Basket"))
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision detected with Basket");

            // Instantiate the new prefab at the apple's position and rotation
            //Instantiate(newPrefab, transform.position, transform.rotation);

            // Destroy the current apple GameObject
            Destroy(gameObject);

            // Increase the score
            score++;

            if (score == 10)
            {
                Debug.Log("You win!");
                FindFirstObjectByType<FadeOut>().Play(() => SceneManager.LoadScene("Level2"));
            }

        }
    }
}
