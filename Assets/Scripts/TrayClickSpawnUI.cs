using UnityEngine;
using UnityEngine.EventSystems;

public class TrayClickSpawnUI : MonoBehaviour, IPointerDownHandler
{
    public GameObject smallFoodUI;

    public void OnPointerDown(PointerEventData eventData)
    {
        smallFoodUI.SetActive(true);

        smallFoodUI.transform.position = eventData.position;
    }
}
