using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float scale = 1.1f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1, 1, 1);
    }
}
