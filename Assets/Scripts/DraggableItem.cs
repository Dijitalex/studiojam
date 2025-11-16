using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform trayRect; 
    public RectTransform dropAreaRect; 
    public GameObject foodPrefab;

    private Vector3 trayHomePos;
    private Canvas parentCanvas;

    private bool dragging = false;

    void Start()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        trayHomePos = transform.position;
    }

    void Update()
    {
        Camera cam = parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null : parentCanvas.worldCamera;
            
        if (!dragging)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(trayRect, Input.mousePosition, cam))
            {
                transform.position = Input.mousePosition;
            }
            else
            {
                transform.position = trayHomePos;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false; 

        Camera cam = parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay
            ? null : parentCanvas.worldCamera;

        if (RectTransformUtility.RectangleContainsScreenPoint(dropAreaRect, eventData.position, cam))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
            worldPos.z = 0;
            Instantiate(foodPrefab, worldPos, Quaternion.identity);
        }
        transform.position = trayHomePos;
    }
}
