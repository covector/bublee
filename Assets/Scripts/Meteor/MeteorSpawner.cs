using UnityEngine;
using System.Collections;

public class MeteorSpawner : MonoBehaviour
{
    [Header("Meteor Prefabs")]
    public GameObject[] meteorPrefabs; // Assign 3 different meteor prefab references in the Inspector

    [Header("Spawn Settings")]
    public float spawnInterval = 1f;     // Time between spawns
    public float minX = -10f;           // Left boundary of spawn position (world space)
    public float maxX = 10f;            // Right boundary of spawn position (world space)
    public float spawnY = 6f;           // Y position above the screen where meteors spawn

    [Header("Meteor Motion Settings")]
    public float minSpeed = 2f;
    public float maxSpeed = 5f;
    public float maxAngle = 20f;  // Max angle variation from straight down

    private void Start()
    {
        // Start spawning meteors continuously
        StartCoroutine(SpawnMeteors());
    }

    private IEnumerator SpawnMeteors()
    {
        while (true)
        {
            SpawnMeteor();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnMeteor()
    {
        // 1. Choose a random meteor from the array
        int randomIndex = Random.Range(0, meteorPrefabs.Length);
        GameObject chosenMeteorPrefab = meteorPrefabs[randomIndex];

        // 2. Random spawn position (horizontal range between minX and maxX, fixed Y = spawnY)
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPos = new Vector3(randomX, spawnY, 0f);

        // 3. Instantiate the meteor
        GameObject meteorObj = Instantiate(chosenMeteorPrefab, spawnPos, Quaternion.identity);

        // 4. Calculate a random angle near "straight down"
        //    e.g., 0 degrees is straight down, +/- maxAngle for variation
        float angle = Random.Range(-maxAngle, maxAngle);

        // 5. Random speed
        float speed = Random.Range(minSpeed, maxSpeed);

        // 6. Pass these values to the meteor script to handle movement
        Meteor meteorScript = meteorObj.GetComponent<Meteor>();
        if (meteorScript != null)
        {
            meteorScript.SetFallParameters(angle, speed);
        }
    }
}
