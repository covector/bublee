using System.Collections.Generic;
using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    public enum Direction { x, y, z }
    public Direction scrollDirection;
    public GameObject spritePrefab;
    int currentChunk = 0;
    float chunkSize;
    Transform cam;
    List<GameObject> chunks = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;
        switch (scrollDirection)
        {
            case Direction.x:
                chunkSize = spritePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
                break;
            case Direction.y:
                chunkSize = spritePrefab.GetComponent<SpriteRenderer>().bounds.size.y;
                break;
            case Direction.z:
                chunkSize = spritePrefab.GetComponent<SpriteRenderer>().bounds.size.z;
                break;
        }
        for (int i = 0; i < 3; i++)
        {
            chunks.Add(Instantiate(spritePrefab));
        }
        UpdateChunk();
    }

    // Update is called once per frame
    void Update()
    {
        float pos = 0;
        switch (scrollDirection)
        {
            case Direction.x:
                pos = cam.position.x;
                break;
            case Direction.y:
                pos = cam.position.y;
                break;
            case Direction.z:
                pos = cam.position.z;
                break;
        }
        int newChunk = Mathf.FloorToInt(pos / chunkSize);
        if (newChunk != currentChunk)
        {
            currentChunk = newChunk;
            UpdateChunk();
        }
    }

    void UpdateChunk()
    {
        Vector3 one = Vector3.zero;
        switch (scrollDirection)
        {
            case Direction.x:
                one = Vector3.right;
                break;
            case Direction.y:
                one = Vector3.up;
                break;
            case Direction.z:
                one = Vector3.forward;
                break;
        }
        chunks[0].transform.position = (currentChunk - 0.5f) * chunkSize * one;
        chunks[1].transform.position = (currentChunk + 0.5f) * chunkSize * one;
        chunks[2].transform.position = (currentChunk + 1.5f) * chunkSize * one;
    }
}
