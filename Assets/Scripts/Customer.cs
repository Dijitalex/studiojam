using System.Collections.Generic;
using UnityEngine;
public enum Order
{
    Worm,
    Fish,
    Food3,
    Food4,
    Food5
}

[System.Serializable]
public class Customer : MonoBehaviour
{
    public List<Order> orders = new List<Order>();
    public List<RectTransform> orderImages; //3 pre-set icons to match food sprites to
    private Dictionary<Order, Sprite> spriteMap;

    //Change as foods are added
    [SerializeField] private Sprite wormSprite;
    [SerializeField] private Sprite fishSprite; 
    [SerializeField] private Sprite food3Sprite;
    [SerializeField] private Sprite food4Sprite;
    [SerializeField] private Sprite food5Sprite;

    public const float basePatience = 10f;
    public List<Order> getOrder() => orders;
    public float getPatience() => basePatience * (1 + (orders.Count - 1) * 0.5f);
    private void Awake()
    {
        spriteMap = new Dictionary<Order, Sprite>
        {
            { Order.Worm, wormSprite },
            { Order.Fish, fishSprite },
            { Order.Food3, food3Sprite },
            { Order.Food4, food4Sprite },
            { Order.Food5, food5Sprite }
        };

        //Generate 1-3 items for customer order
        int numOrders = Random.Range(1, 3);
        for (int i = 0; i < numOrders; i++)
        {
            Order randomOrder = (Order)Random.Range(0, System.Enum.GetValues(typeof(Order)).Length);
            orders.Add(randomOrder);
        }
        LayoutOrder();

    }

    public void LayoutOrder()
    {
        foreach (var icon in orderImages)
            icon.gameObject.SetActive(false);

        if (orders.Count == 1)
        {
            //Change sprite
            orderImages[0].anchoredPosition = Vector2.zero;
            orderImages[0].gameObject.SetActive(true);
        }
        else if (orders.Count == 2)
        {
            orderImages[0].anchoredPosition = new Vector2(-25, 0);
            orderImages[1].anchoredPosition = new Vector2(25, 0);

            orderImages[0].gameObject.SetActive(true);
            orderImages[1].gameObject.SetActive(true);
        }
        else if (orders.Count == 3)
        {
            orderImages[0].gameObject.SetActive(true);
            orderImages[1].gameObject.SetActive(true);
            orderImages[2].gameObject.SetActive(true);
        }
    }


}
