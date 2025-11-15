using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startpos;
    private Canvas parentCanvas;
    public RectTransform dropAreaRect;

    private void Start()
    {
        parentCanvas = GetComponentInParent<Canvas>();
        startpos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out pos
        );

        transform.position = parentCanvas.transform.TransformPoint(pos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 itemPos = transform.position;
        if (RectTransformUtility.RectangleContainsScreenPoint(dropAreaRect, itemPos, eventData.pressEventCamera))
        {
            return;
        }
        else
        {
            transform.position = startpos;
        }
    }
}
