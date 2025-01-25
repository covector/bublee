using System.Collections.Generic;
using UnityEngine;

public class InfiniteSlide : MonoBehaviour
{
    public GameObject spritePrefab;
    public float speed = 1;
    private float offset = 0f;
    int currentChunk = 0;
    float chunkSize;
    Transform cam;
    List<GameObject> chunks = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.transform;
        chunkSize = spritePrefab.GetComponent<SpriteRenderer>().bounds.size.y;

        for (int i = 0; i < 2; i++)
        {
            chunks.Add(Instantiate(spritePrefab));
        }
        UpdateChunk();
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;
        if (offset >= chunkSize)
        {
            offset -= chunkSize;
        }

        UpdateChunk();
    }

    void UpdateChunk()
    {
        chunks[0].transform.position = new Vector3(cam.position.x, cam.position.y - offset, 0);
        chunks[1].transform.position = new Vector3(cam.position.x, currentChunk + chunkSize - offset, 0);
    }
}
