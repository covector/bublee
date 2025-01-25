using UnityEngine;

public class FloorAppleDie : MonoBehaviour
{


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
}
