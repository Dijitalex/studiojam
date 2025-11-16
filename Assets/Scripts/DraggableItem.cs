using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform dropAreaRect; 
    public GameObject foodPrefab; 

    private Vector3 startpos;
    private Canvas parentCanvas;

    void Start()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        startpos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 uiPos = transform.position;

        if (RectTransformUtility.RectangleContainsScreenPoint(dropAreaRect, uiPos, eventData.pressEventCamera))
        {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        worldPos.z = 0;

        Instantiate(foodPrefab, worldPos, Quaternion.identity);
        }

        transform.position = startpos;
    }
}
