using UnityEngine;

    public class BubbleAppleBasket : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Basket"))
        {
            Debug.Log("Collision detected with Basket");

            // Instantiate the new prefab at the apple's position and rotation
            //Instantiate(newPrefab, transform.position, transform.rotation);

            // Destroy the current apple GameObject
            Destroy(gameObject);
        }
    }
}
