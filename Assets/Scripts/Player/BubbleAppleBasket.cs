using UnityEngine;

public class BubbleAppleBasket : MonoBehaviour
{

    //public GameObject newPrefab;
    void OnTriggerEnter2D(Collider2D other)
    {
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
