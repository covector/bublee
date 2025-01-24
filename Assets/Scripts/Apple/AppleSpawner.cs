using UnityEngine;
using System.Collections;
using static Utils;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;

public class AppleSpawner : MonoBehaviour
{
    //public BoxCollider2D topWall ;
    //public BoxCollider2D rightWall;
    //public BoxCollider2D bottomWall;
    //public BoxCollider2D leftWall;
    Vector2 areaMin;
    Vector2 areaMax;
    public GameObject prefab;
    public int maxSpawn = 10;
    //public float spawnDelay = 1f;
    //public Vector2 paddingX;
    //public Vector2 paddingY;
    private List<GameObject> instantiatedObjects = new List<GameObject>();
    public float appleShakeDuration = 1.5f; // Total duration of shaking
    public float appleShakeAngle = 30f; // Maximum angle to rotate during shaking
    public float appleShakeSpeed = 20f; // Speed of rotation
    public float dropAppleInterval = 2.5f;

    void Start()
    {
        //areaMax = new Vector2(rightWall.bounds.min.x - paddingX.y, topWall.bounds.min.y - paddingY.y);
        //areaMin = new Vector2(leftWall.bounds.max.x + paddingX.x, bottomWall.bounds.max.y + paddingY.x);
        areaMax = new Vector2(0.39f, 2.24f);
        areaMin = new Vector2(-5.8f, -1.92f);
        Spawn();
        new WaitForSeconds(0.5f);
        StartCoroutine(DropAppleAtIntervals(dropAppleInterval));
    }

    public void Spawn()
    {
        for (int i = 0; i < maxSpawn; i++)
        {
            Debug.Log(i);
            Vector2 pos = new Vector2(UnityEngine.Random.Range(areaMin.x, areaMax.x), UnityEngine.Random.Range(areaMin.y, areaMax.y));
            GameObject obj = Instantiate(prefab, pos, Quaternion.identity, transform);
            obj.GetComponent<Rigidbody2D>().simulated = false;
            instantiatedObjects.Add(obj);
        }
    }


    private IEnumerator DropAppleAtIntervals(float interval)
    {
        while (instantiatedObjects.Count > 0) // Keep dropping apples as long as there are apples
        {
            DropRandomApple(); // Call the method to drop a random apple
            yield return new WaitForSeconds(interval); // Wait for the specified interval
        }

        Debug.LogWarning("No more apples to drop!");
    }

    [ContextMenu("Call DropRandomObject")]
    public void DropRandomApple()
    {
        // Check if there are any apples in the list
        if (instantiatedObjects.Count == 0)
        {
            Debug.LogWarning("No objects to drop!");
            return;
        }

        // Randomly select an object from the list
        int randomIndex = UnityEngine.Random.Range(0, instantiatedObjects.Count);
        GameObject selectedApple = instantiatedObjects[randomIndex];
        StartCoroutine(ShakeApple(selectedApple));
    }

    private IEnumerator ShakeApple(GameObject obj)
    {
        float appleElapsedTime = 0f;

        // Store the original rotation
        Quaternion originalRotation = obj.transform.localRotation;

        while (appleElapsedTime < appleShakeDuration)
        {
            // Calculate the rotation angle for the shake
            float angle = Mathf.Sin(Time.time * appleShakeSpeed) * appleShakeAngle;

            // Apply the rotation
            obj.transform.localRotation = originalRotation * Quaternion.Euler(0, 0, angle);

            // Increment elapsed time
            appleElapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Reset to the original rotation after shaking
        obj.transform.localRotation = originalRotation;
        obj.GetComponent<Rigidbody2D>().simulated = true; // drop apple
        instantiatedObjects.Remove(obj);
    }

    //public override List<Vector2> SamplePoints(float chunkSize, Vector3 globalPosition, int seed)
    //{
    //    FastPoissonDiskSampling fpds = new FastPoissonDiskSampling(chunkSize, chunkSize, chunkSize / 2f, seed: seed);
    //    return fpds.fill();
    //}

    //[ContextMenu("Call ClearSpawning")]
    //public void ClearSpawning()
    //{
    //    foreach (GameObject obj in instantiatedObjects)
    //    {
    //        Destroy(obj);
    //    }
    //    instantiatedObjects.Clear();
    //}

}