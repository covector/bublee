using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
    private Camera mainCamera;
    private Renderer objectRenderer;

    void Start()
    {
        mainCamera = Camera.main;
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (objectRenderer.isVisible)
        {
            return;
        }

        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        if (screenPoint.y > 1 || screenPoint.y < 0 || screenPoint.x > 1 || screenPoint.x < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        Destroy(gameObject);
    }

}