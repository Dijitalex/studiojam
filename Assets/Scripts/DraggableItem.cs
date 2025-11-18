using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public RectTransform trayRect; 
    public RectTransform dropAreaRect; 
    public GameObject foodPrefab;

    private Vector3 trayHomePos;
    private Canvas parentCanvas;

    private bool dragging = false;
    private Image foodImage;

    void Start()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        trayHomePos = transform.position;

        foodImage = GetComponent<Image>();
        if (foodImage != null)
        {
            Color color = foodImage.color;
            color.a = 0f;
            foodImage.color = color;
        }
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

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown called!");
        if (foodImage != null)
        {
            Color color = foodImage.color;
            color.a = 1f;
            foodImage.color = color;
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        if (foodImage != null)
        {
            Color color = foodImage.color;
            color.a = 1f;
            foodImage.color = color;
        }
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
        if (foodPrefab == null)
        {
            return;
        }
        
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
        worldPos.z = 0;
        
        GameObject newFood = Instantiate(foodPrefab, worldPos, Quaternion.identity);
        
        SpriteRenderer sr = newFood.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder = 100;
        }
    }
    
    transform.position = trayHomePos;
    
    if (foodImage != null)
    {
        Color color = foodImage.color;
        color.a = 0f;
        foodImage.color = color;
    }
}
}
