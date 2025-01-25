using UnityEngine;

public class AppleBubbleCollid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    public GameObject newPrefab; // Assign the new prefab in the Inspector

    public GameObject newPrefabRotten;


    void OnCollisionEnter2D (Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Bubble"))
        {
            Debug.Log("Collision detected with Bubble");

            // Instantiate the new prefab at the apple's position and rotation
            Instantiate(newPrefab, transform.position, transform.rotation);

            // Destroy the current apple GameObject
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Collision detected with Ground");

            // Instantiate the new prefab at the apple's position and rotation
            Instantiate(newPrefabRotten, transform.position, transform.rotation);

            // Destroy the current apple GameObject
            Destroy(gameObject);
        }
    }

   
}
