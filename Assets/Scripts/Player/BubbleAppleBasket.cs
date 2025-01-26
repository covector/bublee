using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

    public class BubbleAppleBasket : MonoBehaviour
{

    public GameObject newPrefab;

    void OnCollisionEnter2D(Collision2D collision)
    {
        //FloorAppleDie otherScript = newPrefab.GetComponent<FloorAppleDie>();

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

            //otherScript.score++;
            //newPrefab.score++;
            //Debug.Log("Score: " + score);

            


            

        }
    }
}
