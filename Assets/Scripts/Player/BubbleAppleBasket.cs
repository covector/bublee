using UnityEngine;

    public class BubbleAppleBasket : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("BubbleApple Trigger detected with " + other.gameObject.name);

        if (other.gameObject.CompareTag("Basket"))
        {
            Debug.Log("BubbleApple Trigger detected with Basket");

            // Instantiate the new prefab at the apple's position and rotation
            //Instantiate(newPrefab, transform.position, transform.rotation);

            // Destroy the current BubbleApple GameObject
            Destroy(gameObject);
        }
    }
}
